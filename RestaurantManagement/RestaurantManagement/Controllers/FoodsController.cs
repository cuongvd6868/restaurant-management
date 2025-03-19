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

        [HttpPost]
        public Task<Food> CreateFood(Food food)
        {
            return foodService.CreateFood(food);
        }

        [HttpGet("{id}")]
        public Task<Food> GetFoodById(int id)
        {
            return foodService.GetFoodById(id);
        }

        [HttpPut]
        public Task<Food> UpdateFood(Food food)
        {
            return foodService.UpdateFood(food);
        }

        [HttpDelete]
        public Task<bool> DeleteFood(int id)
        {
            return foodService.DeleteFood(id);
        }
    }
}
