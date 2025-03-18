using RestaurantManagement.Models;

namespace RestaurantManagement.DAOs.Impl
{
    public class PaymentMethodDAO : GenericDAO<PaymentMethod>, IPaymentMethodDAO
    {
        public PaymentMethodDAO(FoodDbContext context) : base(context)
        {
        }
    }
}
