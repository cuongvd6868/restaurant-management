
using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodOrderDAO : GenericDAO<FoodOrder>, IFoodOrderDAO    {

        public FoodOrderDAO(FoodDbContext context) : base(context)
        {
        }
    }
    }
