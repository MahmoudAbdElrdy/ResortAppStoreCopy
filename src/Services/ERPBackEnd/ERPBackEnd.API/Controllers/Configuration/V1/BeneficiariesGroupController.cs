using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using Configuration.Dto;
using Configuration.Entities;
using Configuration.Repository;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Configuration.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]

    public class BeneficiariesGroupController : MainController<BeneficiariesGroup, BeneficiariesGroupDto, long>

    {
        private readonly IBeneficiariesGroupRepository _repository;


        public BeneficiariesGroupController(GMappRepository<BeneficiariesGroup, BeneficiariesGroupDto, long> mainRepos, IMapper mapper,
           IBeneficiariesGroupRepository repository, IConfiguration configuration)
           : base(mainRepos)
        {
            _repository = repository;
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<BeneficiariesGroupDto> Create([FromBody] BeneficiariesGroupDto input)
        {
            return await _repository.CreateBeneficiariesGroup(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<BeneficiariesGroupDto> Update([FromBody] BeneficiariesGroupDto input)
        {
            return await _repository.UpdateBeneficiariesGroup(input);
        }
        public override async Task<BeneficiariesGroupDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        [HttpDelete("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }
       
    }
}
