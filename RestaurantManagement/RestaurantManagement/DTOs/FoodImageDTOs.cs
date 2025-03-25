namespace RestaurantManagement.DTOs
{
    public class FoodImageDTOs
    {
        public int FoodID { get; set; }
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageLink { get; set; }
    }
}
