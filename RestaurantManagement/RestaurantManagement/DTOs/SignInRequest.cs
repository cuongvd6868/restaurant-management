using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.DTOs
{
    public class SignInRequest
    {
        
        public string Email { get; set; }

      
        public string Password { get; set; }
    }
}
