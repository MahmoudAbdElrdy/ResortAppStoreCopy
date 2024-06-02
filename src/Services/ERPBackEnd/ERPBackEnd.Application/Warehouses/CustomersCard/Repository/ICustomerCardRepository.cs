using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Repository
{
    public interface ICustomerCardRepository
    {

       public Task<PaginatedList<CustomerCardDto>> GetAllCardList(Paging paging );
    }
}
