using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs
{
    public interface IFoodDAO : IGenericDAO<Food>
    {
        public Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize);
        public Task<List<FoodCategory>> GetFoodCategories();
    }
}
