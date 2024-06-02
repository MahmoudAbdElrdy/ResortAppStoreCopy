using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountGroups.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountGroups.Repository
{
    public interface IAccountGroupRepository
    {
        Task<List<TreeDto>> GetTrees(GetAllAccountGroupTreeCommand request);
        Task<string> GetLastCode(GetLastCode request);
        Task<AccountGroupDto> Create(AccountGroupDto request);
        Task<AccountGroupDto> Update(AccountGroupDto request);
        Task<AccountGroupDto> DeleteAccountGroup(DeleteAccountGroupCommand request);
    }
}
