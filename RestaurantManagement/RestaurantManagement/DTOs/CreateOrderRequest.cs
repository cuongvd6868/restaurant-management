namespace RestaurantManagement.DTOs
{
    public class CreateOrderRequest
    {
        public int PaymentMethod { get; set; }
        public string StatusOrder { get; set; }
    }
}
