namespace RestaurantManagement.Models
{

    public class Customer
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string ActiveID { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public bool IsBlock { get; set; }
        public ICollection<FoodFavorite> FoodFavorites { get; set; }
        public ICollection<FoodOrder> FoodOrders { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<FoodFeedback> FoodFeedbacks { get; set; }
    }
}