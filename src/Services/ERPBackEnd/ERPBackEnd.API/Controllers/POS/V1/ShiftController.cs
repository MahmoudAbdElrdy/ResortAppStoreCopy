using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.PriceLists.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class ShiftController : MainController<ShiftMaster, ShiftMasterDto, long>
    {
        private IShiftMasterRepository _repository;
        private IShiftDetailsRepository _shiftDetailsRepository;


        public ShiftController(
            GMappRepository<ShiftMaster, ShiftMasterDto, long> mainRepos, IMapper mapper,
            IShiftMasterRepository repository, IShiftDetailsRepository shiftDetailsRepository) : base(mainRepos)
        {
            _repository = repository;
            _shiftDetailsRepository = shiftDetailsRepository;

        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<ShiftMasterDto> Create([FromBody] ShiftMasterDto input)
        {
            return await _repository.CreateShift(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<ShiftMasterDto> Update([FromBody] ShiftMasterDto input)
        {
            return await _repository.UpdateShift(input);
        }
        public override async Task<ShiftMasterDto> GetById(long id)
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

       
        [HttpGet("getShiftDetailsOfDefaultShift")]

        public async Task<ShiftMasterDto> GetById()
        {
            return await _repository.GetShiftDetailsOfDefaultShift();
        }


    }
}
