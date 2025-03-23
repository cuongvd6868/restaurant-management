using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;
using System.Security.Claims;

namespace RestaurantManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IFoodOderRepository _foodOderRepository;
        private IFoodOderDetailRepository _foodOderDetailRepository;
        private IFoodRepository _foodRepository;
        private ICartItemRepository _cartItemRepository;

        public OrdersController(IFoodOderRepository foodOderRepository, IFoodOderDetailRepository foodOderDetailRepository, IFoodRepository foodRepository, ICartItemRepository cartItemRepository)
        {
            _foodOderRepository = foodOderRepository;
            _foodOderDetailRepository = foodOderDetailRepository;
            _foodRepository = foodRepository;
            _cartItemRepository = cartItemRepository;
        }

        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(CreateOrderRequest request )
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized("User is not logged in");
            var cartItems = await _cartItemRepository.GetListCartItemsByCurrentUser(userId.Value);
            decimal total = (decimal)cartItems.Sum(x => x.Quantity * x.Price);
            FoodOrder foodOrder = new FoodOrder { UserID = userId.Value, PaymentMethodID = request.PaymentMethod, Status = request.StatusOrder, TotalPrice =  total};
            foodOrder = await _foodOderRepository.AddAsync(foodOrder);
            List<FoodOrderDetail> details = new List<FoodOrderDetail>();
            foreach (var item in cartItems )
            {
                details.Add(new FoodOrderDetail { FoodID = item.FoodID, OrderID = foodOrder.OrderID, PurchaseQuantity = item.Quantity, IsFeedBack = false});
            }
            await _foodOderDetailRepository.AddList(details);
            return Ok();
        }
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userIdClaim != null ? int.Parse(userIdClaim) : (int?)null;
        }

    }
}
