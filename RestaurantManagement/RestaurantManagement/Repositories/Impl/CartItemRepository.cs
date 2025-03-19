using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly FoodDbContext _context;

        public CartItemRepository(FoodDbContext context, IGenericDAO<CartItem> cartItemDAO) : base(cartItemDAO)
        {
            _context = context; 
        }
        public async Task<List<CartItem>> GetListCartItemsByCurrentUser(int userId)
        {
            return await _context.CartItems.Include(x=>x.Food).Where(x=> x.UserID == userId).ToListAsync();   
        }

    }
}
