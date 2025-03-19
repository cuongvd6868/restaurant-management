using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories
{
    public interface IFoodImageRepository : IGenericRepository<FoodImage>
    {
        Task<FoodImage?> GetByFoodIdAsync(int foodId);
    }
}
