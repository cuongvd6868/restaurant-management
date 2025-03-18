
using RestaurantManagement.DTOs;
using RestaurantManagement.Models;
using System.Collections.Generic;

namespace RestaurantManagement.Repositories
{
    public interface IUserRepository
    {
        Task<UserCreationResponse> CreateUser(UserCreationRequest request);
        Task<Customer> GetByEmail(string email);
        Task<Role> GetRole(Customer account);
        Task<Customer> GetById(int id);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Update(Customer customer);

    }
}
