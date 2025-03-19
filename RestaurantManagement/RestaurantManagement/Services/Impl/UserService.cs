

using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
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

        public async Task<Customer> BanById(int id)
        {
            Customer cus = await _userRepository.GetById(id);
            if(cus.IsBlock.Value)
            {
                cus.IsBlock = false;
            }
            else
            {
                cus.IsBlock = true;
            }
            await _userRepository.Update(cus);
            return cus;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<Customer> Update(Customer customer)
        {
            return await _userRepository.Update(customer);
        }
    }
}
