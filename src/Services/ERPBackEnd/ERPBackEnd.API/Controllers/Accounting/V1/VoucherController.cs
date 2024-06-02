using AutoMapper;
using Common.BaseController;
using Common.Constants;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class VoucherController : MainController<Voucher, VoucherDto, long>
    {
        private IVoucherRepository _repository;

        public VoucherController(GMappRepository<Voucher, VoucherDto, long> mainRepos, IMapper mapper, 
            IVoucherRepository repository, IConfiguration configuration) 
            : base(mainRepos)
        {
            _repository = repository;
        }


        [HttpPost("addWithPermission")]
        [SuccessResultMessage("createSuccessfully")]

        public  async Task<VoucherDto> Create( [FromQuery] long id, [FromBody] VoucherDto input)
        {
            return await _repository.CreateVoucher(input);
        }

        [HttpPost("editWithPerssion")]
        [SuccessResultMessage("editSuccessfully")]
        public  async Task<VoucherDto> Update([FromQuery] long id, [FromBody] VoucherDto input)
        {
            return await _repository.UpdateVoucher(input);
        }

        [HttpGet("deleteWithPermission")]
        [SuccessResultMessage("deleteSuccessfully")]
        public  Task Delete(long id, long voucherId)
        {

            return _repository.DeleteAsync(voucherId);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }

        public override async Task<VoucherDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        #region getLastCodeByTypeId 
        [HttpGet("getLastCodeByTypeId")]
        public async Task<IActionResult> getLastCodeByTypeId(long typeId)
        {
            string Lastcode = await _repository.getLastVoucherCodeByTypeId(typeId);

            return Ok(new { message = "Last Voucher Code:", data = Lastcode });
        }

        #endregion

        #region getNotGenerateEntryVouchers 
        [HttpGet("getNotGenerateEntryVouchers")]
        public IActionResult GetNotGenerateEntryVouchers()
        {
            var NotGenerateEntryVouchers = _repository.GetNotGenerateEntryVouchers();
            return Ok(new { message = "NotGenerateEntryVouchers:", data = NotGenerateEntryVouchers });
        }

        #endregion
        [HttpPost("generateEntry")]
        public async Task GenerateEntry([FromBody] List<long> ids)
        {
            await _repository.generateEntry(ids);
        }

        [HttpGet]
        [Route("checkPrint")]
        
        public bool Print(long id)
        {
            return true;
        }
        [HttpGet("getVouchersBillPays")]

        public async Task<ResponseResult<List<BillPaymentDto>>> GetVouchersBillPays(long VoucherId)
        {
            return await _repository.GetVouchersBillPays(VoucherId);
        }

    }
}
