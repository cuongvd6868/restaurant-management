

using RestaurantManagement.DTOs;
using RestaurantManagement.Repositories;

namespace RestaurantManagement.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserCreationResponse> CreateUser(UserCreationRequest request)
        {
            return await _userRepository.CreateUser(request);
        }
    }
}
