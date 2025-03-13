using Microsoft.AspNetCore.Identity;

namespace RestaurantManagement.Models
{
    public class Role : IdentityRole<int>
    {
        public string Decriptions { get; set; }
    }
}