using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodImageDAO : GenericDAO<FoodImage>, IFoodImageDAO
    {
        public FoodImageDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
