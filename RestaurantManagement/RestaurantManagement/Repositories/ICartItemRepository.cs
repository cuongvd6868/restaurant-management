using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        public  Task<List<CartItem>> GetListCartItemsByCurrentUser(int userId);
    }
}
