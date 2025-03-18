using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Services
{
    public interface IFoodService
    {
        public Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize);
        public Task<List<FoodCategory>> GetFoodCategories();
    }
}
