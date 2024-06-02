using AutoMapper;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Periods.Repository
{
    public class PeriodRepository : IPeriodRepository
    {
        public PeriodRepository(IMapper mapper,
            IGRepository<Period> context)
        {
          
        }

       
    }
}
