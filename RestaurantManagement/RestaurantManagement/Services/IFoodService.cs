using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;

namespace RestaurantManagement.Services
{
    public interface IFoodService
    {
        public Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize);
        public Task<List<FoodCategory>> GetFoodCategories();
        public Task<Food> CreateFoodAsync(Food food);
        public Task<Food> UpdateFoodAsync(Food food);

        public Task<bool> DeleteFoodAsync(int foodId);
        public Task<Food> GetOne(int foodId);
        Task<PagedListAPI<FoodViewModel>> GetFoodsAPI(int? cateId, int pageNumber, int pageSize);
    }
}
