using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.POS.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CashTransferController : MainController<CashTransfer, CashTransferDto, long>
    {
        private ICashTransferRepository _repository;

        public CashTransferController(GMappRepository<CashTransfer, CashTransferDto, long> mainRepos, ICashTransferRepository repository) : base(mainRepos)
        {
            _repository = repository;
        }
        [HttpPost("create")]
        [SuccessResultMessage("createSuccessfully")]

        public async Task<CashTransferDto> Create([FromBody] CashTransferDto input)
        {
            return await _repository.CreateCashTransfer(input);
        }
        [HttpPost("update")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<CashTransferDto> Update([FromBody] CashTransferDto input)
        {
            return await _repository.UpdateCashTransfer(input);
        }

    }
}
