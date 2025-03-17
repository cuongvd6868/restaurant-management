using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using System.Linq.Expressions;

namespace RestaurantManagement.DAOs.Impl
{
    public class FoodDAO : GenericDAO<Food>, IFoodDAO
    {
        public FoodDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
