

using RestaurantManagement.DTOs;

namespace RestaurantManagement.Services
{
    public interface IAuthService
    {
        Task<SignInResponse> SignIn(SignInRequest request);
        Task Logout();
    }
}
