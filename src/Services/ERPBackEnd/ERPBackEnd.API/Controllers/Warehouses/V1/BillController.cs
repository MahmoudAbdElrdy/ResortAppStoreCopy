using AutoMapper;
using Common.BaseController;
using Common.Constants;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class BillController : MainController<Bill, BillDto, long>
    {
        private IBillRepository _repository;

        public BillController(
            GMappRepository<Bill, BillDto, long> mainRepos, IMapper mapper,
            IBillRepository repository) : base(mainRepos)
        {
            _repository = repository;
            
        }
        [HttpPost("addWithPermission")]
       // [SuccessResultMessage("createSuccessfully")]
        public  async Task<BillDto> Create([FromQuery] long id, [FromBody] BillDto input)
        {
            return await _repository.CreateBill(input);
        }
        [HttpPost("editWithPerssion")]
        [SuccessResultMessage("editSuccessfully")]
        public  async Task<BillDto> Update([FromQuery] long id, [FromBody] BillDto input)
        {
            return await _repository.UpdateBill(input);
        }
        public override async Task<BillDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        [HttpPost("generateEntry")]

        public async Task GenerateEntry([FromBody] List<long> ids)
        {
            await _repository.generateEntry(ids);
        }
        [HttpPost("postToWarehouses")]

        public async Task PostToWarehouses([FromBody] List<long> ids)
        {
            await _repository.postToWarehouses(ids);
        }

        [HttpGet("getNotGeneratedEntryBills")]
        
        public  async Task<ResponseResult<List<NotGenerateEntryBillDto>>> GetNotGeneratedEntryBills()
        {
           return  await _repository.ExecuteGetNotGeneratedEntryBills();
        }
        [HttpGet("getNotPostToWarehousesAutomaticallyBills")]

        public async Task<ResponseResult<List<NotPostToWarehousesAutomaticallyBillDto>>> GetNotPostToWarehousesAutomaticallyBills()
        {
            return await _repository.ExecuteGetNotPostToWarehousesAutomaticallyBills();
        }

        [HttpGet("getUnsyncedElectronicBills")]

        public async Task<ResponseResult<List<UnSyncedElectronicBillsDto>>> GetUnsyncedElectronicBills()
        {
            return await _repository.GetUnsyncedElectronicBills();
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
            string Lastcode = await _repository.getLastBillCodeByTypeId(typeId);

            return Ok(new { message = "Last Bill Code:", data = Lastcode });
        }

        #endregion
        [HttpGet]
        [Route("getBillDynamicDeterminant")]
        public async Task<BillDynamicDeterminantList> GetById([FromQuery] BillDynamicDeterminantInput input)
        {
            return await _repository.GetBillDynamicDeterminantList(input);
        } 
        [HttpPost("CheckQuantity")]
      //  [Route("CheckQuantity")]
        public async Task<bool> Create([FromBody] List<InsertBillDynamicDeterminantDto> input) 
        {
            return await _repository.CheckQuantity(input);
        }
        [HttpGet("getBillPayments")]

        public async Task<ResponseResult<List<BillPaymentDto>>> GetBillPayments(int? BillKind, long? CustomerId, long? SupplierId,long? VoucherTypeId)
        {
            return await _repository.GetBillPayments(BillKind,CustomerId,SupplierId, VoucherTypeId);
        }
        [HttpGet("getBillPaid")]

        public async Task<ResponseResult<List<BillPaymentDto>>> GetBillPaid(int? BillKind, long? CustomerId, long? SupplierId)
        {
            return await _repository.GetBillPaid(BillKind, CustomerId, SupplierId);
        }

        [HttpGet("getInstallmentPaid")]

        public async Task<ResponseResult<List<BillPaymentDto>>> GetInstallmentPaid(int? BillKind, long? CustomerId, long? SupplierId)
        {
            return await _repository.GetInstallmentPaid(BillKind, CustomerId, SupplierId);
        }
        [HttpGet]
        [Route("getDynamicDeterminantList")]
        public async Task<BillDynamicDeterminantList> GetById([FromQuery] InventoryDynamicDeterminantInput input)
        {
            return await _repository.GetDynamicDeterminantList(input);
        }


    }
}
