using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAOs;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Services;
using RestaurantManagement.ViewModel;

namespace RestaurantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : Controller
    {
        private IFoodService _foodService;
        private FoodDbContext _context;

        public FoodsController(IFoodService foodService, FoodDbContext context)
        {
            this._foodService = foodService;
            _context = context;
        }

        [HttpGet("FoodDetail/{foodId}")]
        public async Task<IActionResult> FoodDetail(int foodId)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.FoodID == foodId);

            if (food == null)
            {
                return NotFound("Food not found");
            }

            return View("~/Views/Foods/FoodDetail.cshtml", food);
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
        [Route("CreateFoodAsync")]
        public async Task<Food> CreateFoodAsync(Food food)
        {
            return await _foodService.CreateFoodAsync(food);
        }
        [Route("UpdateFoodAsync")]
        [HttpPut]
        public async Task<Food> UpdateFoodAsync(Food food)
        {
            return await _foodService.UpdateFoodAsync(food);
        }
        [Route("DeleteFoodAsync")]
        public async Task<bool> DeleteFoodAsync(int foodId)
        {
            return await _foodService.DeleteFoodAsync(foodId);
        }
        [Route("GetOne")]
        public async Task<Food> GetOne(int foodId)
        {
            return await _foodService.GetOne(foodId);
        }

        // all food types
        [Route("FoodTypeAll")]
        public async Task<IActionResult> FoodTypeAll()
        {
            var model = new FoodTypeAllViewModel
            {
                BreakfastFoods = await _foodService.GetFoodsAPI(1, 1, 3), // Breakfast (cateId = 1)
                LunchFoods = await _foodService.GetFoodsAPI(2, 1, 3),     // Lunch (cateId = 2)
                DinnerFoods = await _foodService.GetFoodsAPI(3, 1, 3),    // Dinner (cateId = 3)
                DessertFoods = await _foodService.GetFoodsAPI(4, 1, 3)    // Dessert (cateId = 4)
            };

            return View("~/Views/Foods/FoodTypeAll.cshtml", model);
        }
        [Route("GetFoodAll")]
        public async Task<IActionResult> GetFoodAll(int? cateId, string? search, int? pageNumber, int? pageSize)
        {
            int index = pageNumber == null ? 1 : (int)pageNumber;
            int size = pageSize == null ? 9 : (int)pageSize;
            var foods = await _foodService.GetFooodsAsync(cateId, search, index, size);
            ViewBag.foods = foods;
            ViewBag.Search = search;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = size;
            ViewBag.TotalPages = (int)Math.Ceiling((double)foods.TotalCount / size);
            return View("~/Views/Foods/GetFoodAll.cshtml");
        }

        [HttpGet("get-food-morning")]
        public async Task<IActionResult> GetFoodMorning(int? cateId = 1, int pageNumber = 1, int pageSize = 3)
        {
            var foods = await _foodService.GetFoodsAPI(cateId, pageNumber, pageSize);
            return View("~/Views/Foods/FoodType/Breakfast.cshtml", foods);
        }
        [HttpGet("get-food-lunch")]
        public async Task<IActionResult> GetFoodLunch(int? cateId = 2, int pageNumber = 1, int pageSize = 3)
        {
            var foods = await _foodService.GetFoodsAPI(cateId, pageNumber, pageSize);
            return View("Lunch", foods);
        }
        [HttpGet("get-food-dinner")]
        public async Task<IActionResult> GetFoodDinner(int? cateId = 3, int pageNumber = 1, int pageSize = 3)
        {
            var foods = await _foodService.GetFoodsAPI(cateId, pageNumber, pageSize);
            return View("Dinner", foods);
        }
        [HttpGet("get-food-dessert")]
        public async Task<IActionResult> GetFoodDessert(int? cateId = 4, int pageNumber = 1, int pageSize = 3)
        {
            var foods = await _foodService.GetFoodsAPI(cateId, pageNumber, pageSize);
            return View("Dessert", foods);
        }



    }
}
