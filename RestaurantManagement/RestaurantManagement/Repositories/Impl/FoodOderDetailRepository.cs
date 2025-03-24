using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{

    public class FoodOderDetailRepository : GenericRepository<FoodOrderDetail>, IFoodOderDetailRepository
    {
        private readonly FoodDbContext _context;

        public FoodOderDetailRepository(IGenericDAO<FoodOrderDetail> FoodOrderDetail, FoodDbContext context) : base(FoodOrderDetail)
        {
            _context = context;
        }

        public async Task AddList(List<FoodOrderDetail> orderDetails)
        {
            await _context.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FoodOrderDetail>> GetListDetailByOId(int orderId)
        {
            return await _context.FoodOrderDetails.Include(x => x.Food).Where(x => x.OrderID == orderId).ToListAsync();
        }
    }

}

