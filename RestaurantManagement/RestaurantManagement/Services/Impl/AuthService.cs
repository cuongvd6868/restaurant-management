using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using RestaurantManagement.Repositories;

namespace RestaurantManagement.Services.Impl
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher<Customer> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(
            IUserRepository userRepository,
            IJwtService jwtService,
            IPasswordHasher<Customer> passwordHasher,
            IHttpContextAccessor httpContextAccessor
           )
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _passwordHasher = passwordHasher;
        }

        public async Task Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }

            httpContext.Response.Cookies.Delete("refreshToken");
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            var user = await _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                return new SignInResponse
                {
                    IsSuccess = false,
                    Message = "Email is not exit"
                };
            }

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (passwordVerificationResult != PasswordVerificationResult.Success)
            {
                return new SignInResponse
                {
                    IsSuccess = false,
                    Message = "Wrong password"
                };
            }

            Role role = await _userRepository.GetRole(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var accessToken = _jwtService.GenerateAccessToken(claims);
            var refreshToken = _jwtService.GenerateRefreshToken(claims);

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new InvalidOperationException("HttpContext is not available.");
            }
            httpContext.Response.Cookies.Append("accessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict, // Ngăn chặn CSRF
                Expires = DateTimeOffset.UtcNow.AddDays(14) // Thời gian sống của cookie
            });

            return new SignInResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                IsSuccess = true,
                Message = "Login Successful",
                Email = user.Email,
                Name = user.LastName + " " + user.FirstName,
                Role = role.Name
            };
        }
    }
}
