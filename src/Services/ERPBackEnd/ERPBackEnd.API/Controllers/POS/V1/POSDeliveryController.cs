using AutoMapper;
using Common.BaseController;
using Common.Constants;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class POSDeliveryController : MainController<POSDeliveryMaster, POSDeliveryMasterDto, long>
    {
        private IPOSDeliveryMasterRepository _repository;
        private IPOSDeliveryDetailsRepository _posDeliveryDetailsRepository;


        public POSDeliveryController(
            GMappRepository<POSDeliveryMaster, POSDeliveryMasterDto, long> mainRepos, IMapper mapper,
            IPOSDeliveryMasterRepository repository, IPOSDeliveryDetailsRepository posDeliveryDetailsRepository) : base(mainRepos)
        {
            _repository = repository;
            _posDeliveryDetailsRepository = posDeliveryDetailsRepository;

        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]
        public override async Task<POSDeliveryMasterDto> Create([FromBody] POSDeliveryMasterDto input)
        {
            return await _repository.CreatePOSDelivery(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public override async Task<POSDeliveryMasterDto> Update([FromBody] POSDeliveryMasterDto input)
        {
            return await _repository.UpdatePOSDelivery(input);
        }
        public override async Task<POSDeliveryMasterDto> GetById(long id)
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

        [HttpGet("calculatePOSDelivery")]

        public async Task<ResponseResult<List<CalculatePOSDeliveryDto>>> GetAll(string DateFrom, string DateTo)
        {
            return await _repository.CalculatePOSDelivery(DateFrom, DateTo);
        }




    }
}
