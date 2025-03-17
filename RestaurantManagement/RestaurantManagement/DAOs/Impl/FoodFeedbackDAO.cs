using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodFeedbackDAO : GenericDAO<FoodFeedback>, IFoodFeedbackDAO
    {
        public FoodFeedbackDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
