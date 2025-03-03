namespace RestaurantManagement.Models
{
    public class FoodCategory
    {
        public int FoodCategoryID { get; set; }
        public string FoodCategoryName { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}