using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodOrderDetailDAO : GenericDAO<FoodOrderDetail>, IFoodOrderDetailDAO
    {
        public FoodOrderDetailDAO(FoodDbContext context) : base(context){
        }
    }
}
