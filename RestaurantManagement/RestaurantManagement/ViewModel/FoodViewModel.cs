namespace RestaurantManagement.ViewModel
{
    public class FoodViewModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Price { get; set; }
        public string FoodCategoryName { get; set; } // Tên danh mục
        public List<string> ImageLinks { get; set; } 
    }
}
