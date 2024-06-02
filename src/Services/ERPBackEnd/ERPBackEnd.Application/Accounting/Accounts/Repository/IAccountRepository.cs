using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Accounts.Repository
{
    public interface IAccountRepository
    {
        Task<List<AccountTreeDto>> GetTrees(GetAllAccountTreeCommand request);
        Task<string> GetLastCode(GetLastCode request);
        Task<AccountDto> Create(AccountDto request);
        Task<AccountDto> Update(AccountDto request);
        Task<AccountDto> DeleteAccount(DeleteAccountCommand request);
        Task<IEnumerable<Account>> GetLeafAccounts(long AccountClassificationId);
        Task<bool> IsLeafAccount(DeleteAccountCommand request);
        Task<List<AccountBalanceDto>> GetAccountBalalnce(string accountId);
    }
}
