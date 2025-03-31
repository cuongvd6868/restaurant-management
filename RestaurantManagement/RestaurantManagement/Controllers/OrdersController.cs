using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RestaurantManagement.DTOs;
using RestaurantManagement.Hubs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using System.Security.Claims;

namespace RestaurantManagement.Controllers
{
    //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private IFoodOderRepository _foodOderRepository;
        private IFoodOderDetailRepository _foodOderDetailRepository;
        private IFoodRepository _foodRepository;
        private ICartItemRepository _cartItemRepository;
        

        public OrdersController(IFoodOderRepository foodOderRepository,
            IFoodOderDetailRepository foodOderDetailRepository,
            IFoodRepository foodRepository,
            ICartItemRepository cartItemRepository)
        {
            _foodOderRepository = foodOderRepository;
            _foodOderDetailRepository = foodOderDetailRepository;
            _foodRepository = foodRepository;
            _cartItemRepository = cartItemRepository;
            
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(CreateOrderRequest request)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");
            var cartItems = await _cartItemRepository.GetListCartItemsByCurrentUser(userId.Value);
            decimal total = (decimal)cartItems.Sum(x => x.Quantity * x.Price);
            FoodOrder foodOrder = new FoodOrder { UserID = userId.Value, Address = "", PaymentMethodID = request.PaymentMethod, Status = request.StatusOrder, TotalPrice = total };
            foodOrder = await _foodOderRepository.AddAsync(foodOrder);
            List<FoodOrderDetail> details = new List<FoodOrderDetail>();
            foreach (var item in cartItems)
            {
                details.Add(new FoodOrderDetail { FoodID = item.FoodID, OrderID = foodOrder.OrderID, PurchaseQuantity = item.Quantity, IsFeedBack = false });
            }
            await _foodOderDetailRepository.AddList(details);
            await _cartItemRepository.RemoveRange(cartItems);
           
            return Ok();
        }
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;
        }
        //api/Orders/GetListOrders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Auth");

            var orders = await _foodOderRepository.FindByConditionAsync(x => x.UserID == userId);
            return View(orders);
        }


        [HttpGet]
        [Route("GetListOrderDetails")]
        public async Task<IActionResult> GetListOrderDetails(int orderId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            var Items = await _foodOderDetailRepository.GetListDetailByOId(orderId);

            return Ok(Items);
        }

        [HttpGet]
        [Route("GetListOrders")]
        public async Task<IActionResult> GetListOrders()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");

            var Items = await _foodOderRepository.GetListAlll();

            return Ok(Items);
        }

        [HttpPut]
        [Route("ChangeOrderStatus")]
        public async Task<IActionResult> ChangeOrderStatus(ChangeOrderStatusRequest request)
        {
            //var userId = GetUserId();
            //if (userId == null) return Unauthorized("User is not logged in");

            var Item = await _foodOderRepository.GetByIdAsync(request.OrderId);
            if (Item == null) return NotFound("This orderId not found");
            Item.Status = request.StatusOrder;
            await _foodOderRepository.UpdateAsync(Item);
            return Ok(Item);
        }
    }
}
