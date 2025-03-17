using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodCategoryDAO :GenericDAO<FoodCategory> , IFoodCategoryDAO
    {
        public FoodCategoryDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
