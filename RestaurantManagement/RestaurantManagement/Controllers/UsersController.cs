using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.DTOs;
using RestaurantManagement.Services;

namespace RestaurantManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService,
            IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationRequest request)
        {
            var result = await _userService.CreateUser(request);
            if (result == null)
            {
                return BadRequest("Email đã tồn tại!");
            }
            return Ok(result);
        }
    }
}
