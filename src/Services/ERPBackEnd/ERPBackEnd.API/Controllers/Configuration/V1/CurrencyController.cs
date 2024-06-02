using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Currencies.Dto;
using ERPBackEnd.API.Helpers;
using Common.Repositories;
using Configuration.Entities;
using Common.Exceptions;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Currencies.Repository;
namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CurrencyController : MainController<Currency, CurrencyDto, long>
    {
        private ICurrencyRepository _repositoryCurrency; 
        public CurrencyController(GMappRepository<Currency, CurrencyDto, long> mainRepos, ICurrencyRepository repositoryCurrency) : base(mainRepos)
        {
            _repositoryCurrency = repositoryCurrency;
        }
        public override Task<CurrencyDto> GetById(long id)
        {
            return _repositoryCurrency.GetByCurrencyId(new GetByCurrencyId() { Id = id });
        }
        [HttpPost("addTransaction")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<CurrencyTransactionDto>> Add([FromBody] CreateCurrencyTransactionCommand command)
        {
            return Ok((await _repositoryCurrency.CreateCurrencyTransactionCommand(command)));
        }
        //[AllowAnonymous]
        [HttpPost("editTransaction")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<CurrencyTransactionDto>> Edit([FromBody] EditCurrencyTransactionCommand command)
        {

            return Ok((await _repositoryCurrency.EditCurrencyTransactionCommand(command)));
        }
        [HttpGet("deleteTransaction")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<CurrencyDto>> Delete(long id)
        {
            return Ok(await _repositoryCurrency.DeleteCurrencyTransactionCommand(new DeleteCurrencyTransactionCommand() { Id = id }));
        }
        [HttpPost("deleteListTransaction")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteListCurrencyTransactionCommand command) 
        {
            return Ok(await _repositoryCurrency.DeleteListCurrencyTransactionCommand(command));
        }
        [HttpGet("getByIdTransaction")]

        public async Task<ActionResult<CurrencyTransactionDto>> GetByCurrencyTransactionId(long id)
        {
            return Ok(await _repositoryCurrency.GetByCurrencyTransactionId(new GetByCurrencyTransactionId() { Id = id }));
        }
        [HttpGet("GetCurrenciesTransactions")]
        public async Task<ActionResult<List<CurrencyTransactionDto>>> GetCurrenciesTransactions()
        {
            return Ok((await _repositoryCurrency.GetAllCurrenciesTransactionsQueries()));
        }
        public override async Task<ActionResult<List<CurrencyDto>>> GetDDL()
        {
            return Ok((await _repositoryCurrency.GetAllCurrencies()));
        }

    }

}
