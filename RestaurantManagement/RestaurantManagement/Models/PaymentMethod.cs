namespace RestaurantManagement.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal PaymentCost { get; set; }

        // Quan hệ 1 - nhiều với FoodOrder
        public ICollection<FoodOrder> FoodOrders { get; set; }
    }
}