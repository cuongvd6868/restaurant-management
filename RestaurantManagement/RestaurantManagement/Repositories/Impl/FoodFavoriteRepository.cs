using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodFavoriteRepository : GenericRepository<FoodFavorite>, IFoodFavoriteRepository
    {
        public FoodFavoriteRepository(IGenericDAO<FoodFavorite> dao) : base(dao)
    {
    }
}
}
