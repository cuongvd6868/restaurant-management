using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Services;

namespace RestaurantManagement.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService,
            IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginHandle([FromBody] SignInRequest request)
        {
            var result = await _authService.SignIn(request);
            if (result == null)
            {
                return BadRequest("Somethings went wrong");
            }
            return Ok(result);
        }


    }
}
