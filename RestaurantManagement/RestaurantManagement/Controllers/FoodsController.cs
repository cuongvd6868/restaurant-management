using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DAOs;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Services;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private IFoodService foodService;

        public FoodsController(IFoodService foodService)
        {
            this.foodService = foodService;
        }
        [Route("GetFooodsAsync")]
        public async Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize)
        {
            return await foodService.GetFooodsAsync(cateId, search, pageNumber, pageSize);
        }
        [Route("GetFoodCategories")]
        public async Task<List<FoodCategory>> GetFoodCategories()
        {
            return await foodService.GetFoodCategories();
        }

        public async Task<Food> CreateFoodAsync(Food food)
        {
            return await foodService.CreateFoodAsync(food);
        }
        public async Task<Food> UpdateFoodAsync(Food food)
        {
            return await foodService.UpdateFoodAsync(food);
        }

        public async Task<bool> DeleteFoodAsync(int foodId)
        {
            return await foodService.DeleteFoodAsync(foodId);
        }
        public async Task<Food> GetOne(int foodId)
        {
            return await foodService.GetOne(foodId);
        }
    }
}
