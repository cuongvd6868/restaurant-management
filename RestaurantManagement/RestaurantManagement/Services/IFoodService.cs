using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;

namespace RestaurantManagement.Services
{
    public interface IFoodService
    {
        public Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize);
        public Task<List<FoodCategory>> GetFoodCategories();
        public Task<Food> GetFoodById(int id);
        public Task<Food> CreateFood(Food food);
        public Task<Food> UpdateFood(Food food);
        public Task<bool> DeleteFood(int id);
        Task<PagedListAPI<FoodViewModel>> GetFoodsAPI(int? cateId, int pageNumber, int pageSize);
    }
}
