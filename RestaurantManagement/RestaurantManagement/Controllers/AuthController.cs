using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Services;
using Microsoft.AspNetCore.Http;


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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete("accessToken");
            Response.Cookies.Delete(".AspNetCore.Cookies");

            return RedirectToAction("Login", "Auth");
        }



    }
}
