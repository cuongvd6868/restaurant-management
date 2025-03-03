namespace RestaurantManagement.Models
{

    public class CartItem
    {
        public int CartItemID { get; set; }
        public int UserID { get; set; }
        public int FoodID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Food Food { get; set; }
        public Customer Customer { get; set; }
    }
}