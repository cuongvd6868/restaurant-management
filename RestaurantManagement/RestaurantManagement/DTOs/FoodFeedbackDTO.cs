namespace RestaurantManagement.DTOs
{
    public class FoodFeedbackDTO
    {
        public int FoodFeedbackID { get; set; }
        public int FoodID { get; set; }
        public int RatingPoint { get; set; }
        public string Comment { get; set; }
    }
}
