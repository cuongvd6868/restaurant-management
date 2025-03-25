using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodImageRepository : GenericRepository<FoodImage>, IFoodImageRepository
    {
        private readonly IFoodImageDAO _dao;
        public FoodImageRepository(IFoodImageDAO dao) : base(dao)
        {
            _dao = dao;
        }

        public async Task<FoodImage?> GetByFoodIdAsync(int foodId)
        {
            return await _dao.GetByFoodIdAsync(foodId);
        }
    }
}
