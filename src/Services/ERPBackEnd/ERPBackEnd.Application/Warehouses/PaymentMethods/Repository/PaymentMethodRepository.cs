using AutoMapper;
using ResortAppStore.Repositories;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PaymentMethods.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        public PaymentMethodRepository(IMapper mapper,
            IGRepository<PaymentMethod> context)
        {
     
        }

       
    }
}
