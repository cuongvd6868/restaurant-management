using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories
{
    public interface IFoodOderRepository : IGenericRepository<FoodOrder>
    {
        public Task<List<FoodOrder>> GetListAlll();
    }
}
