using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class PriceListController : MainController<PriceListMaster, PriceListMasterDto, long>
    {
        private IPriceListMasterRepository _repository;

        public PriceListController(
            GMappRepository<PriceListMaster, PriceListMasterDto, long> mainRepos, IMapper mapper,
            IPriceListMasterRepository repository) : base(mainRepos)
        {
            _repository = repository;
            
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<PriceListMasterDto> Create([FromBody] PriceListMasterDto input)
        {
            return await _repository.CreatePriceList(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<PriceListMasterDto> Update([FromBody] PriceListMasterDto input)
        {
            return await _repository.UpdatePriceList(input);
        }
        public override async Task<PriceListMasterDto> GetById(long id)
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
