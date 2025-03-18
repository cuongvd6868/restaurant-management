using Microsoft.AspNetCore.Identity;
using System.Security.Principal;

namespace RestaurantManagement.Models
{

    public class Customer : IdentityUser<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public bool? IsBlock { get; set; }
        public ICollection<FoodFavorite> FoodFavorites { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<FoodFeedback> FoodFeedbacks { get; set; }
    }
}