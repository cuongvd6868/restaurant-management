using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodOderRepository : GenericRepository<FoodOrder>, IFoodOderRepository
    {
        private readonly FoodDbContext _context;

        public FoodOderRepository(IGenericDAO<FoodOrder> foodOderDAO, FoodDbContext context) : base(foodOderDAO)
        {
            _context = context;
        }

        public async Task<List<FoodOrder>> GetListAlll()
        {
            return await _context.FoodOrders.Include(x => x.Customer).Include(x => x.PaymentMethod).ToListAsync();
        }
    }
}
