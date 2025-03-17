using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodImageRepository : GenericRepository<FoodImage>, IFoodImageRepository
    {
        public FoodImageRepository(IGenericDAO<FoodImage> dao) : base(dao)
        {
        }
    }
}
