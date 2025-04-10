﻿using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories
{
    public interface IFoodOderDetailRepository : IGenericRepository<FoodOrderDetail>
    {
        public Task AddList(List<FoodOrderDetail> orderDetails);
        public Task<List<FoodOrderDetail>> GetListDetailByOId(int orderId);
    }
}
