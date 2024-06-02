
using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;


namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ManualInventoryApprovalController : MainController<ManualInventoryApproval, ManualInventoryApprovalDto, long>
    {
        private IManualInventoryApprovalRepository _repository;

        public ManualInventoryApprovalController(GMappRepository<ManualInventoryApproval, ManualInventoryApprovalDto, long> mainRepos, IMapper mapper,
            IManualInventoryApprovalRepository repository, IConfiguration configuration) : base(mainRepos)
        {
            _repository = repository;
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]

        public async Task<ManualInventoryApprovalDto> Create([FromBody] ManualInventoryApprovalDto input)
        {
            return await _repository.CreateManualInventoryApproval(input);
        }

        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]

        public async Task<ManualInventoryApprovalDto> Update([FromBody] ManualInventoryApprovalDto input)
        {
            return await _repository.UpdateManualInventoryApproval(input);
        }

        [HttpDelete("delete")]
        [SuccessResultMessage("deleteSuccessfully")]

        public Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        [HttpDelete("deleteListEntity")]
        [SuccessResultMessage("deleteSuccessfully")]


        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }



    }
}
