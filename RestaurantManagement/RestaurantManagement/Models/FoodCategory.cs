using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    public class FoodCategory
    {
        public int FoodCategoryID { get; set; }
        public string FoodCategoryName { get; set; }
        [JsonIgnore]
        public ICollection<Food> Foods { get; set; }
    }
}