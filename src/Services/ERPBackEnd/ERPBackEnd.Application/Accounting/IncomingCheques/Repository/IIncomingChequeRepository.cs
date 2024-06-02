using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IncomingCheques.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public interface IIncomingChequeRepository
    {
        Task<IncomingChequeMasterDto> CreateIncomingCheque(IncomingChequeMasterDto input);
        Task<IncomingChequeMasterDto> UpdateIncomingCheque(IncomingChequeMasterDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<IncomingChequeMasterDto> FirstInclude(long id);
        Task GenerateEntryActions(long Id, int action);
        Task CancelEntryActions(long Id, int action);
    }
}
