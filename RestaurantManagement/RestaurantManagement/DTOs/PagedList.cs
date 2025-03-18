using RestaurantManagement.Models;

namespace RestaurantManagement.DTOs
{
    public class PagedList
    {
        public List<Food> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
