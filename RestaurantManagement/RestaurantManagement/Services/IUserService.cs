
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Services
{ 
    
    public interface IUserService
    {
        Task<UserCreationResponse> CreateUser(UserCreationRequest request);
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Update(Customer customer);
        Task<Customer> BanById(int id);
    }
}
