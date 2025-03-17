using RestaurantManagement.DAOs;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodRepository : GenericRepository<Food>, IFoodRepository
    {
        public FoodRepository(IGenericDAO<Food> foodDAO) : base(foodDAO)
        {
        }
    }
}
