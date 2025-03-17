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
            // Xóa session đăng nhập (nếu có)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Xóa cookie authentication
            Response.Cookies.Delete(".AspNetCore.Cookies");

            // Xóa accessToken trong cookie
            if (Request.Cookies["accessToken"] != null)
            {
                Response.Cookies.Append("accessToken", "", new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(-1), // Hết hạn ngay lập tức
                    HttpOnly = true, // Đảm bảo cookie HTTP-Only bị xóa
                    Secure = Request.IsHttps, // Phải chạy trên HTTPS nếu Secure=true
                    SameSite = SameSiteMode.None, // Đặt lại SameSite cho đúng với cấu hình ban đầu
                    Path = "/" // Đảm bảo xóa trên toàn bộ trang
                });
            }

            return RedirectToAction("Login", "Auth");
        }

    }
}
