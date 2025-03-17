using Microsoft.AspNetCore.Identity;

namespace RestaurantManagement.Models
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}