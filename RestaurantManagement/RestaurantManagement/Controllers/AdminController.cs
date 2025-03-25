using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.Repositories;
using RestaurantManagement.Services;

namespace RestaurantManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly IFoodService _foodService;

        public AdminController(IFoodService foodService)
        {
            _foodService = foodService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Food(int? cateId, string? search, int pageNumber = 1, int pageSize = 5)
        {
            var foods = await _foodService.GetFooodsAsync(cateId, search, pageNumber, pageSize);
            return View(foods);
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Analytics()
        {
            return View();
        }
    }
}
