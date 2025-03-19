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
    public class FoodsController : Controller
    {
        private IFoodService _foodService;

        public FoodsController(IFoodService foodService)
        {
            this._foodService = foodService;
        }

        [Route("GetFooodsAsync")]
        public async Task<PagedList> GetFooodsAsync(int? cateId, string? search, int pageNumber, int pageSize)
        {
            return await _foodService.GetFooodsAsync(cateId, search, pageNumber, pageSize);
        }

        [Route("GetFoodCategories")]
        public async Task<List<FoodCategory>> GetFoodCategories()
        {
            return await _foodService.GetFoodCategories();
        }


        [HttpGet("get-food-morning")]
        public async Task<IActionResult> GetFoodMorning(int? cateId = 1, int pageNumber = 1, int pageSize = 3)
        {
            var foods = await _foodService.GetFoodsAPI(cateId, pageNumber, pageSize);
            return View("FoodCheck", foods);
        }

    }
}
