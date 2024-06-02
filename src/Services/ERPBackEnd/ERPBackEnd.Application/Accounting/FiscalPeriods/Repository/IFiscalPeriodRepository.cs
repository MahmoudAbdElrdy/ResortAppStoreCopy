using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Dto;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Repository
{
    public interface IFiscalPeriodRepository
    {
        Task<ActionResult<FiscalPeriodDto>> Edit([FromBody] FiscalPeriodDto command);
        Task close(long fiscalPeriodId, string fromDateFisCalPeriod, string toDateFisCalPeriod, string closeAccountId);
        Task open(long fiscalPeriodId);

    }
}
