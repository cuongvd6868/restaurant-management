using RestaurantManagement.DAOs;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    
        public class FoodOderDetailRepository : GenericRepository<FoodOrderDetail>, IFoodOderDetailRepository
        {
            public FoodOderDetailRepository(IGenericDAO<FoodOrderDetail> FoodOrderDetail) : base(FoodOrderDetail)
            {
            }
        }

    }

