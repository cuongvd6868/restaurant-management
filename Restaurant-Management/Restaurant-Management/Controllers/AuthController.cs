using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Management.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
