﻿namespace RestaurantManagement.DTOs
{
    public class PagedListAPI<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalCount / PageSize;
    }
}
