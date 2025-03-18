using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodFavoriteDAO : GenericDAO<FoodFavoriteDAO>, IFoodFavoriteDAO
    {
        public FoodFavoriteDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
