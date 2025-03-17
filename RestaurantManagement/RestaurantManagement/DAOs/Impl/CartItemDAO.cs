using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class CartItemDAO : GenericDAO<CartItem>, ICartItemDAO
    {
        public CartItemDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
