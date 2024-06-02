using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public interface IJournalEntriesRepository
    {
        
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<JournalEntriesMasterDto> FirstInclude(long id);
        Task<int> UpdateListAsync(List<long> ids);
        Task<PaginatedList<JournalEntriesMasterDto>> GetAllList(Paging paging);
        Task<List<JournalEntriesDto>> GetJournalEntries();
        Task<List<JournalEntriesDto>> GetJournalEntryById(long Id);
    }
}
