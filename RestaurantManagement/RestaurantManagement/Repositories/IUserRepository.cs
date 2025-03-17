
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories
{
    public interface IUserRepository
    {
        Task<UserCreationResponse> CreateUser(UserCreationRequest request);
        Task<Customer> GetByEmail(string email);
        Task<Role> GetRole(Customer account);
    }
}
