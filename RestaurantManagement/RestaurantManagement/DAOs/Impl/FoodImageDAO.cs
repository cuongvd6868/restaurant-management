using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodImageDAO : GenericDAO<FoodImage>, IFoodImageDAO
    {

        private readonly FoodDbContext _context;
        public FoodImageDAO(FoodDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FoodImage?> GetByFoodIdAsync(int foodId)
        {
            return await _context.FoodImages.FirstOrDefaultAsync(fi => fi.FoodID == foodId);
        }
    }
}
