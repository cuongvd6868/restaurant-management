using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(IGenericDAO<CartItem> cartItemDAO) : base(cartItemDAO)
        {
        }
    }
}
