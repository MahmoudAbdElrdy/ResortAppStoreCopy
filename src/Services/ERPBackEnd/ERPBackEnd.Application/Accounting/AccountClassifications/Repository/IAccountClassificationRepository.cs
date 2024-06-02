using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountClassifications.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.AccountClassificationes.Repository
{
    public interface IAccountClassificationRepository
    {
        Task<AccountClassificationDto> Update(AccountClassificationDto request);
        Task<AccountClassificationDto> Create(AccountClassificationDto request);

    }
}
