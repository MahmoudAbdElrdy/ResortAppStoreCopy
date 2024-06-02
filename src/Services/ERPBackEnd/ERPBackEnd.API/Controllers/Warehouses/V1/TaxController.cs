using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Taxes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class TaxController : MainController<TaxMaster, TaxMasterDto, long>
    {
        private ITaxMasterRepository _repository;

        public TaxController(
            GMappRepository<TaxMaster, TaxMasterDto, long> mainRepos, IMapper mapper,
            ITaxMasterRepository repository) : base(mainRepos)
        {
            _repository = repository;
            
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<TaxMasterDto> Create([FromBody] TaxMasterDto input)
        {
            return await _repository.CreateTax(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<TaxMasterDto> Update([FromBody] TaxMasterDto input)
        {
            return await _repository.UpdateTax(input);
        }
        public override async Task<TaxMasterDto> GetById(long id)
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
