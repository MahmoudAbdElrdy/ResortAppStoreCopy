using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.BankAccounts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.BankAccountes.Repository
{
    public interface IBankAccountRepository
    {
      
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<BankAccountDto> FirstInclude(long id);
        Task<BankAccountDto> CreateEntity(BankAccountDto input);
        Task<PaginatedList<BankAccountDto>> GetAllIncluding(Paging paging);
        Task<BankAccountDto> UpdateEntity(BankAccountDto input);
    }
}
