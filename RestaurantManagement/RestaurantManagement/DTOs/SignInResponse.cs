namespace RestaurantManagement.DTOs
{
    public class SignInResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
        public bool IsSuccess { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }

    }
}
