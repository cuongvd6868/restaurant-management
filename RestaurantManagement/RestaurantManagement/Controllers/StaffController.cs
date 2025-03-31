using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Repositories;
using System.Threading.Tasks;

namespace RestaurantManagement.Controllers
{
    public class StaffController : Controller
    {
        private readonly IFoodOderRepository _foodOrderRepository;

        public StaffController(IFoodOderRepository foodOrderRepository)
        {
            _foodOrderRepository = foodOrderRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _foodOrderRepository.GetListAlll());
        }
    }
}
