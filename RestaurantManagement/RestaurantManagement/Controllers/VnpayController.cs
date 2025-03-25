using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IVnpay _vnpay;
        private IFoodOderRepository _foodOderRepository;
        private IFoodOderDetailRepository _foodOderDetailRepository;
        private IFoodRepository _foodRepository;
        private ICartItemRepository _cartItemRepository;
        public VnpayController(IConfiguration configuration , IVnpay vnpay, IFoodOderRepository foodOderRepository, IFoodOderDetailRepository foodOderDetailRepository, IFoodRepository foodRepository, ICartItemRepository cartItemRepository)
        {
            _vnpay = vnpay;
            _configuration = configuration;
            _foodOderRepository = foodOderRepository;
            _foodOderDetailRepository = foodOderDetailRepository;
            _foodRepository = foodRepository;
            _cartItemRepository = cartItemRepository;
            _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:ReturnUrl"]);
        }

        [HttpGet("CreatePaymentUrl")]
        public ActionResult<string> CreatePaymentUrl(double moneyToPay, string description)
        {
            try
            {
                var ipAddress = NetworkHelper.GetIpAddress(HttpContext); // Lấy địa chỉ IP của thiết bị thực hiện giao dịch

                var request = new PaymentRequest
                {
                    PaymentId = DateTime.Now.Ticks,
                    Money = moneyToPay,
                    Description = description,
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY, // Tùy chọn. Mặc định là tất cả phương thức giao dịch
                    CreatedDate = DateTime.Now, // Tùy chọn. Mặc định là thời điểm hiện tại
                    Currency = Currency.VND, // Tùy chọn. Mặc định là VND (Việt Nam đồng)
                    Language = DisplayLanguage.Vietnamese // Tùy chọn. Mặc định là tiếng Việt
                };

                var paymentUrl = _vnpay.GetPaymentUrl(request);

                return Created(paymentUrl, paymentUrl);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                    if (paymentResult.IsSuccess)
                    {
                        return Ok();
                    }

                    return BadRequest("Thanh toán thất bại");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return NotFound("Không tìm thấy thông tin thanh toán.");
        }

        [HttpGet("Callback")]
        public async Task<IActionResult> Callback(string vnp_OrderInfo)
        {
            if (Request.QueryString.HasValue)
            {
                try
                {
                    var paymentResult = _vnpay.GetPaymentResult(Request.Query);
                    var resultDescription = $"{paymentResult.PaymentResponse.Description}. {paymentResult.TransactionStatus.Description}.";

                    if (paymentResult.IsSuccess)
                    {
                        if (!int.TryParse(vnp_OrderInfo, out int userId) || userId == 0)
                        {
                            return Unauthorized("User is not logged in");
                        }

                        var cartItems = await _cartItemRepository.GetListCartItemsByCurrentUser(userId);
                        decimal total = cartItems.Sum(x => x.Quantity * x.Price);

                        FoodOrder foodOrder = await _foodOderRepository.AddAsync(new FoodOrder
                        {
                            UserID = userId,
                            Address = "",
                            PaymentMethodID = 2,
                            Status = "Paid",
                            TotalPrice = total
                        });

                        List<FoodOrderDetail> details = cartItems.Select(item => new FoodOrderDetail
                        {
                            FoodID = item.FoodID,
                            OrderID = foodOrder.OrderID,
                            PurchaseQuantity = item.Quantity,
                            IsFeedBack = false
                        }).ToList();

                        await _foodOderDetailRepository.AddList(details);
                        await _cartItemRepository.RemoveRange(cartItems);

                        return RedirectToAction("ResultPayment", "Home", new { message = "Thanh toán thành công!" });
                    }

                    return RedirectToAction("ResultPayment", "Home", new { message = resultDescription });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("ResultPayment", "Home", new { message = $"Lỗi: {ex.Message}" });
                }
            }

            return RedirectToAction("ResultPayment", "Home", new { message = "Không tìm thấy thông tin thanh toán." });
        }
    }
}
