using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Repository
{
    public interface IIssuingChequeRepository
    {
        Task<IssuingChequeMasterDto> CreateIssuingCheque(IssuingChequeMasterDto input);
        Task<IssuingChequeMasterDto> UpdateIssuingCheque(IssuingChequeMasterDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<IssuingChequeMasterDto> FirstInclude(long id);
        Task GenerateEntryActions(long id, int action);
        Task CancelEntryActions(long id, int action);
    }
}
