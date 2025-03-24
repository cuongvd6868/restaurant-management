using System.Text.Json.Serialization;

namespace RestaurantManagement.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodID { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal PaymentCost { get; set; }

        // Quan hệ 1 - nhiều với FoodOrder
        [JsonIgnore]
        public ICollection<FoodOrder> FoodOrders { get; set; }
    }
}