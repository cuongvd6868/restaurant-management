
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using System.Configuration;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodOrderDAO : GenericDAO<FoodOrder>, IFoodOrderDAO    {

        private readonly FoodDbContext _context;
        public FoodOrderDAO(FoodDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<FoodOrder>> GetAllAsync()
        {
            return await _context.FoodOrders.Include(fo => fo.Customer)
                .Include(fo => fo.PaymentMethod)
                .ToListAsync();
        }
    }
    }
