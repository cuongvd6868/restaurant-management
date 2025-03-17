using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodOderRepository : GenericRepository<FoodOrder>, IFoodOderRepository
    {
        public FoodOderRepository(IGenericDAO<FoodOrder> foodOderDAO) : base(foodOderDAO)
        {
        }
    }
}
