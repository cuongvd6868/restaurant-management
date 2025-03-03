namespace RestaurantManagement.Models
{

    public class FoodFavorite
    {
        public int FoodFavoriteID { get; set; }
        public int FoodID { get; set; }
        public int UserID { get; set; }
        public Food Food { get; set; }
        public Customer Customer { get; set; }
    }
}