using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodCategoryRepository : GenericRepository<FoodCategory>,IFoodCategoryRepository
    {
        public FoodCategoryRepository(IGenericDAO<FoodCategory> FoodCategory) : base(FoodCategory)
        {
        }
    }
}
