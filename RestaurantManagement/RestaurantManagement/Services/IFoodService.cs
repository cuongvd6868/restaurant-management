using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;

namespace RestaurantManagement.Services
{
    public interface IFoodService
    {
        public Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize);
        public Task<List<FoodCategory>> GetFoodCategories();
        Task<PagedListAPI<FoodViewModel>> GetFoodsAPI(int? cateId, int pageNumber, int pageSize);
    }
}
