using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs
{
    public interface IFoodImageDAO : IGenericDAO<FoodImage>
    {
        Task<FoodImage?> GetByFoodIdAsync(int foodId);
    }
}
