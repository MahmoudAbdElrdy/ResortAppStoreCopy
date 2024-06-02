using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Subscription;
using ResortAppStore.Services.Administration.Application.Subscription.CashPayment.Dto;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CashPaymentInfoController :  MainController<CashPaymentInfo, CashPaymentInfoDto, long>
    {
        private ICashPaymentInfoRepository _repositoryCashPaymentInfo;
        public CashPaymentInfoController(GMappRepository<CashPaymentInfo, CashPaymentInfoDto, long> mainRepos, 
            ICashPaymentInfoRepository repositoryCashPaymentInfo) : base(mainRepos)
        {
            _repositoryCashPaymentInfo = repositoryCashPaymentInfo;
        }

        [HttpGet("getCashPaymentInfo")]
        public async Task<ActionResult<CashPaymentInfoDto>> GetCashPaymentInfoById()
        {
            return Ok(await _repositoryCashPaymentInfo.GetCashPaymentInfo());
        }

      

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public  async Task<ActionResult<CashPaymentInfoDto>> Update([FromBody] CashPaymentInfoDto input)
        {
            return Ok(await _repositoryCashPaymentInfo.EditCashPaymentInfo(input));
        }
    }
}
