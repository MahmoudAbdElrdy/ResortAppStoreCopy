using AutoMapper;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountingPeriods.Repository
{
    public class AccountingPeriodRepository : IAccountingPeriodRepository
    {
        public AccountingPeriodRepository(IMapper mapper,
            IGRepository<AccountingPeriod> context)
        {
          
        }

       
    }
}
