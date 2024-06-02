
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class FiscalPeriodController : MainController<FiscalPeriod, FiscalPeriodDto, long>
    {
        private IFiscalPeriodRepository _fiscalPeriodRepository { get; set; }
        public FiscalPeriodController(GMappRepository<FiscalPeriod, FiscalPeriodDto, long> mainRepos, IFiscalPeriodRepository fiscalPeriodRepository) : base(mainRepos)
        {
            _fiscalPeriodRepository = fiscalPeriodRepository;
        }

        [HttpPost("updateFiscalPeriod")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<FiscalPeriodDto>> Edit([FromBody] FiscalPeriodDto input)
        {
            return Ok(await _fiscalPeriodRepository.Edit(input));
        }

        [HttpGet("close")]
       // [SuccessResultMessage("closeSuccessfully")]
        public async Task close(long fiscalPeriodId, string fromDateFisCalPeriod, string toDateFisCalPeriod, string closeAccountId)
        {
            await _fiscalPeriodRepository.close(fiscalPeriodId, fromDateFisCalPeriod, toDateFisCalPeriod, closeAccountId);
        }
        [HttpGet("open")]
        //[SuccessResultMessage("openSuccessfully")]
        public async Task open(long fiscalPeriodId)
        {
            await _fiscalPeriodRepository.open(fiscalPeriodId);
        }
    }
}
