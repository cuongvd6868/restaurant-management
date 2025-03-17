using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class FoodFeedbackRepository : GenericRepository<FoodFeedback>, IFoodFeedbackRepository
    {
        public FoodFeedbackRepository(IGenericDAO<FoodFeedback> dao) : base(dao)
        {
        }
    }
}
