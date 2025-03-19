using RestaurantManagement.DAOs;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace RestaurantManagement.Services.Impl
{
    public class FoodService : IFoodService
    {
        private IFoodDAO foodDAO;
        private readonly HttpClient _httpClient;

        public FoodService(IFoodDAO foodDAO, HttpClient httpClient)
        {
            this.foodDAO = foodDAO;
            _httpClient = httpClient;
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

        public async Task<PagedListAPI<FoodViewModel>> GetFoodsAPI(int? cateId, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7081/api/Foods/GetFooodsAsync?cateId={cateId}&pageNumber={pageNumber}&pageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PagedListAPI<FoodViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

    }
}
