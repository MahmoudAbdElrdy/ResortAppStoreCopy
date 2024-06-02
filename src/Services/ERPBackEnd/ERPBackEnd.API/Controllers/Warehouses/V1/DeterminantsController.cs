using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class DeterminantsController : MainController<DeterminantsMaster, DeterminantsMasterDto, long>
    {
        private IDeterminantsMasterRepository _repository;

        public DeterminantsController(GMappRepository<DeterminantsMaster, DeterminantsMasterDto, long> mainRepos, IMapper mapper,
            IDeterminantsMasterRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<DeterminantsMasterDto> Create([FromBody] DeterminantsMasterDto input)
        {
            return await _repository.CreateDeterminant(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<DeterminantsMasterDto> Update([FromBody] DeterminantsMasterDto input)
        {
            return await _repository.UpdateDeterminant(input);
        }
        public override async Task<DeterminantsMasterDto> GetById(long id)
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
