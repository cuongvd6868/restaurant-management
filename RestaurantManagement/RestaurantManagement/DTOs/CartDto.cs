namespace RestaurantManagement.DTOs
{
    public class CartDto
    {
        public int foodId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
    }
}
