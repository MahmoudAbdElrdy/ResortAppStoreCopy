using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Units.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Units.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UnitController : MainController<Unit, UnitDto, long>
    {
        private IUnitRepository _repositoryUnit; 
        public UnitController(GMappRepository<Unit, UnitDto, long> mainRepos, IUnitRepository repositoryUnit) : base(mainRepos)
        {
            _repositoryUnit = repositoryUnit;
        }
        public override Task<UnitDto> GetById(long id)
        {
            return _repositoryUnit.GetByUnitId(new GetByUnitId() { Id = id });
        }
        [HttpPost("addTransaction")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<UnitTransactionDto>> Add([FromBody] CreateUnitTransaction command)
        {
            return Ok((await _repositoryUnit.CreateUnitTransaction(command)));
        }
        //[AllowAnonymous]
        [HttpPost("editTransaction")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<UnitTransactionDto>> Edit([FromBody] EditUnitTransaction command)
        {

            return Ok((await _repositoryUnit.EditUnitTransaction(command)));
        }
        [HttpGet("deleteTransaction")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<UnitDto>> DeleteTransaction(long id)
        {
            return Ok(await _repositoryUnit.DeleteUnitTransaction(new DeleteUnitTransaction() { Id = id }));
        }
        [HttpPost("deleteListTransaction")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<int>> DeleteTransaction([FromBody] DeleteListUnitTransaction command)
        {
            return Ok(await _repositoryUnit.DeleteListUnitTransaction(command));
        }
        [HttpGet("getByIdTransaction")]

        public async Task<ActionResult<UnitTransactionDto>> GetByUnitTransactionId(long id)
        {
            return Ok(await _repositoryUnit.GetByUnitTransactionId(new GetByUnitTransactionId() { Id = id }));
        }
        [HttpGet("getUnitTransactions")]
        public async Task<ActionResult<List<UnitTransactionDto>>> GetUnitTransactions()
        {
            return Ok((await _repositoryUnit.GetAllUnitsTransactionsQueries()));
        }


    }

}
