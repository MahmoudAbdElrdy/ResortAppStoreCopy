using AutoMapper;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.POS;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class POSBillController : MainController<POSBill, POSBillDto, long>
    {
        private IPOSBillRepository _repository;

        public POSBillController(
            GMappRepository<POSBill, POSBillDto, long> mainRepos, IMapper mapper,
            IPOSBillRepository repository) : base(mainRepos)
        {
            _repository = repository;
            
        }
        [HttpPost("addWithPermission")]
       // [SuccessResultMessage("createSuccessfully")]
        public  async Task<POSBillDto> Create([FromQuery] long id, [FromBody] POSBillDto input)
        {
            return await _repository.CreatePOSBill(input);
        }
        [HttpPost("editWithPerssion")]
        [SuccessResultMessage("editSuccessfully")]
        public  async Task<POSBillDto> Update([FromQuery] long id, [FromBody] POSBillDto input)
        {
            return await _repository.UpdatePOSBill(input);
        }
        public override async Task<POSBillDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }

        [HttpDelete("deleteWithPermission")]
        [SuccessResultMessage("deleteSuccessfully")]
        public  Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        [HttpPost("deleteList")]
      
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }

        [HttpGet]
        [Route("checkPrint")]

        public bool Print(long id)
        {
            return true;
        }

        #region getLastCodeByTypeId 
        [HttpGet("getLastCodeByTypeId")]
        public async Task<IActionResult> getLastCodeByTypeId(long typeId)
        {
            string Lastcode = await _repository.getLastPOSBillCodeByTypeId(typeId);

            return Ok(new { message = "Last Bill Code:", data = Lastcode });
        }

        #endregion
        [HttpGet]
        [Route("getPOSBillDynamicDeterminant")]
        public async Task<POSBillDynamicDeterminantList> GetById([FromQuery] POSBillDynamicDeterminantInput input)
        {
            return await _repository.GetPOSBillDynamicDeterminantList(input);
        }

      


    }
}
