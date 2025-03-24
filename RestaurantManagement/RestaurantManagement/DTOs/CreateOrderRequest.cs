namespace RestaurantManagement.DTOs
{
    public class CreateOrderRequest
    {
        public int PaymentMethod { get; set; }
        public string StatusOrder { get; set; }
    }
    public class ChangeOrderStatusRequest
    {
        public int OrderId { get; set; }
        public string StatusOrder { get; set; }
    }
}
