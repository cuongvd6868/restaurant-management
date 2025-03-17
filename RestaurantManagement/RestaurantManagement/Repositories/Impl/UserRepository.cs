
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Azure.Core;
using RestaurantManagement.Models;
using RestaurantManagement.DTOs;
using Org.BouncyCastle.Asn1.Ocsp;

namespace RestaurantManagement.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly FoodDbContext _context;
        private readonly IPasswordHasher<Customer> _passwordHasher;
        private readonly ILogger<UserRepository> logger;
        public UserRepository(FoodDbContext context,
            IPasswordHasher<Customer> passwordHasher, ILogger<UserRepository> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            this.logger = logger;   
        }

        public async Task<UserCreationResponse?> CreateUser(UserCreationRequest request)
        {
            // Kiểm tra xem email đã tồn tại chưa
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return null; // Trả về null nếu email đã tồn tại
            }

            // Kiểm tra xem Role đã tồn tại chưa
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Customer");
            if (role == null)
            {
                role = new Role
                {
                    Name = "Customer",
                    Description = "This role is allowed to view the news article (news status must be active) in this system."
                };
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync(); // Lưu role vào DB
            }

            // Tạo user mới
            Customer newUser = new Customer()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                IsBlock = false
                
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password.Trim());

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync(); // Lưu user vào DB để có ID

            // Thêm user vào Role (UserRoles)
            var userRole = new IdentityUserRole<int>
            {
                UserId = newUser.Id,
                RoleId = role.Id
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync(); // Lưu vào DB

            return new UserCreationResponse
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };
        }

        public async Task<Customer> GetByEmail(string email)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            return existingUser;
        }

        public async Task<Role> GetRole(Customer account)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(r => r.UserId == account.Id);

            if (userRole == null)
            {
                return null; // Trả về null nếu không tìm thấy role
            }

            Role role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
            return role;
        }
    }
}
