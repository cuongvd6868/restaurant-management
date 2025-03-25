namespace RestaurantManagement.Models
{
    public class FoodOrder
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public Customer Customer { get; set; }

        public ICollection<FoodOrderDetail> FoodOrderDetails { get; set; }

        public int PaymentMethodID { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}