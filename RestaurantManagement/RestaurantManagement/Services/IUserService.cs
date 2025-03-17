
using RestaurantManagement.DTOs;

namespace RestaurantManagement.Services
{ 
    
    public interface IUserService
    {
        Task<UserCreationResponse> CreateUser(UserCreationRequest request);
    }
}
