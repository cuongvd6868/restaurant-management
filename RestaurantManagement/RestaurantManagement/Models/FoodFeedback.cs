namespace RestaurantManagement.Models
{
    public class FoodFeedback
    {
        public int FoodFeedbackID { get; set; }
        public int FoodID { get; set; }
        public int UserID { get; set; }
        public int RatingPoint { get; set; }
        public string Comment { get; set; }
        public Food Food { get; set; }
        public Customer Customer { get; set; }
    }
}