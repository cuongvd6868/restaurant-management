using RestaurantManagement.DAOs;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Services.Impl
{
    public class FoodService : IFoodService
    {
        private IFoodDAO foodDAO;

        public FoodService(IFoodDAO foodDAO)
        {
            this.foodDAO = foodDAO;
        }

        public async Task<List<FoodCategory>> GetFoodCategories()
        {
            return await foodDAO.GetFoodCategories();
        }

        public async Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize)
        {
            return await foodDAO.GetFooodsAsync(cateId, search, pageNumber, pageSize);
        }
        public async Task<Food> CreateFoodAsync(Food food)
        {
            return await foodDAO.AddAsync(food);
        }
        public async Task<Food> UpdateFoodAsync(Food food)
        {
           return await foodDAO.UpdateAsync(food);
        }

        public async Task<bool> DeleteFoodAsync(int foodId)
        {
            return await foodDAO.DeleteFoodAsync(foodId);
        }
        public async Task<Food> GetOne(int foodId)
        {
            return await foodDAO.GetByIdAsync(foodId);
        }
    }
}
