namespace RestaurantManagement.Models
{

    public class FoodOrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int FoodID { get; set; }
        public int PurchaseQuantity { get; set; }
        public bool IsFeedBack { get; set; }
        public FoodOrder FoodOrder { get; set; }
        public Food Food { get; set; }
    }
}