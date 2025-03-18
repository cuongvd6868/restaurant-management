using RestaurantManagement.DAOs;
using RestaurantManagement.DAOs.Impl;
using RestaurantManagement.Models;

namespace RestaurantManagement.Repositories.Impl
{
    public class PaymentMethodRepository : GenericRepository<PaymentMethod>,IPaymentMethodRepository
    {
        public PaymentMethodRepository(IGenericDAO<PaymentMethod> PaymentMethodDAO) : base(PaymentMethodDAO)
        {
        }
    }
}
