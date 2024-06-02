using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Subscription;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Repository;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodeController : MainController<PromoCodes, PromoCodeDto, long>
    {
        private IPromoCodeRespository _repositoryPromoCode;
        public PromoCodeController(GMappRepository<PromoCodes, PromoCodeDto, long> mainRepos,
            IPromoCodeRespository codeRespository) : base(mainRepos)
        {
            _repositoryPromoCode = codeRespository;
        }

        [HttpGet("getPromoCodeById/{id}")]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCodeById([FromRoute]long id)
        {
            return Ok(await _repositoryPromoCode.GetPromoCodeCommandbyId(id));
        }

       


        [AllowAnonymous]
        [HttpGet("GetPromoCodesByPromoCode/{id}")]
        public async Task<ActionResult<PromoCodeDto>> GetPromoCodesByPromoCode([FromRoute] string id)
        {
            return Ok((await _repositoryPromoCode.GetPromoCodeCommandbyCode(id)));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<PromoCodeDto>> Update([FromBody] PromoCodeDto input)
        {
            return Ok(await _repositoryPromoCode.EditPromoCodeCommand(input));
        }


        [HttpPost("Create")]
        [SuccessResultMessage("addSuccessfully")]
        public async Task<ActionResult<PromoCodeDto>> Create([FromBody] PromoCodeDto input)
        {
            return Ok(await _repositoryPromoCode.CreatePromoCodeCommand(input));
        }


        [HttpPost("Delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<int>> Delete([FromBody] long id)
        {
            return Ok(await _repositoryPromoCode.DeletePromoCodeCommand(id));
        }
    }
}
