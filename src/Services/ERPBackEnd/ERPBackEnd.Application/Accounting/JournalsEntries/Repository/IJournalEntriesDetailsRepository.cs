using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public interface IJournalEntriesDetailsRepository
    {
          Task createEntryDetails(long masterid, byte orderid, string accountid, decimal? credit, decimal? debit, decimal? creditLocal, decimal? debitLocal, decimal? transactionFactor, long? currencyId,long? CostCenterId, long? ProjectId);
    }
}
