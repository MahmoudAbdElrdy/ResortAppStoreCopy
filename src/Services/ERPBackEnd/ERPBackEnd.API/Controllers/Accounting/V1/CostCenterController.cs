using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Dto;
using Common.Repositories;

using ResortAppStore.Services.ERPBackEnd.Application.Accounting.CostCenters.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
   [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CostCenterController : MainController<CostCenter, CostCenterDto, long>
    {
        private ICostCenterRepository _CostCenterRepository { get; set; }
        public CostCenterController(GMappRepository<CostCenter, CostCenterDto, long> mainRepos, ICostCenterRepository CostCenterRepository) : base(mainRepos)
        {
            _CostCenterRepository = CostCenterRepository;
        }
        [HttpGet("all-tree")]
        public async Task<ActionResult<List<CostCenterTreeDto>>> ShowTree([FromQuery] GetAllCostCenterTreeCommand query)
        {
            return Ok(await _CostCenterRepository.GetTrees(query));
        }
        [HttpGet("getLastCodeTree")]

        public async Task<ActionResult<string>> GetLastCode(long? parentId)
        {
            return Ok(await _CostCenterRepository.GetLastCode(new GetLastCode() { ParentId = parentId }));
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<CostCenterDto>> Add([FromBody] CostCenterDto command)
        {
            return Ok(await _CostCenterRepository.Create(command));
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<CostCenterDto>> Edit([FromBody] CostCenterDto command)
        {

            return Ok(await _CostCenterRepository.Update(command));
        }
        [HttpGet("deleteCostCenter")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<CostCenterDto>> Delete(long id) 
        {
            return Ok(await _CostCenterRepository.DeleteCostCenter(new DeleteCostCenterCommand() { Id = id }));
        }
    }
}
