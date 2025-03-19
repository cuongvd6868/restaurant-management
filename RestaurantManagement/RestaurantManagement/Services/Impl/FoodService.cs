using RestaurantManagement.DAOs;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;

namespace RestaurantManagement.Services.Impl
{
    public class FoodService : IFoodService
    {
        private IFoodDAO foodDAO;
        private readonly IFoodRepository foodRepository;

        public FoodService(IFoodDAO foodDAO,
            IFoodRepository foodRepository)
        {
            this.foodDAO = foodDAO;
            this.foodRepository = foodRepository;
        }

        public async Task<Food> CreateFood(Food food)
        {
            return await foodDAO.AddAsync(food);
        }

        public async Task<bool> DeleteFood(int id)
        {
            return await foodDAO.DeleteAsync(id);
        }

        public async Task<Food> GetFoodById(int id)
        {
            
            return await foodDAO.GetByIdAsync(id);
            
        }

        public async Task<List<FoodCategory>> GetFoodCategories()
        {
            return await foodDAO.GetFoodCategories();
        }

        public async Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize)
        {
            return await foodDAO.GetFooodsAsync(cateId, search, pageNumber, pageSize);
        }

        public async Task<Food> UpdateFood(Food food)
        {
            return await foodDAO.UpdateAsync(food);
        }
    }
}
