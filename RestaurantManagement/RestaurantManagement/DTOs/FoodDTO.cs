using RestaurantManagement.Models;

namespace RestaurantManagement.DTOs
{
    public class FoodDTO
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Price { get; set; }
        public ICollection<FoodImageDTOs>? FoodImages { get; set; }
        public ICollection<FoodFavorite>? FoodFavorites { get; set; }
        public ICollection<FoodOrderDetail>? FoodOrderDetails { get; set; }
        public ICollection<FoodFeedback>? FoodFeedbacks { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }
        public int? FoodCategoryID { get; set; }
        public FoodCategory? FoodCategory { get; set; }
    }
}
