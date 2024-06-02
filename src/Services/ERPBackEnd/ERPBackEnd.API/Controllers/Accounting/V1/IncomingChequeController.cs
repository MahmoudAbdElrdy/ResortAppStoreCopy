using AutoMapper;
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IncomingCheques.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class IncomingChequeController : MainController<IncomingChequeMaster, IncomingChequeMasterDto, long>
    {
        private IIncomingChequeRepository _repository;



        public IncomingChequeController(
            GMappRepository<IncomingChequeMaster, IncomingChequeMasterDto, long> mainRepos, IMapper mapper,
            IIncomingChequeRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        public override async Task<IncomingChequeMasterDto> Create([FromBody] IncomingChequeMasterDto input)
        {
            return await _repository.CreateIncomingCheque(input);
        }

        public override async Task<IncomingChequeMasterDto> Update([FromBody] IncomingChequeMasterDto input)
        {
            return await _repository.UpdateIncomingCheque(input);
        }
        public override async Task<IncomingChequeMasterDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        public override Task Delete(long id)
        {

            return _repository.DeleteAsync(id);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }
        [HttpPost("GenerateEntryActions")]
        //[SuccessResultMessage("GenerateEntrySuccessfully")]
        public async Task GenerateEntryActions(long Id, int action)
        {
            await _repository.GenerateEntryActions(Id, action);
        }
        [HttpPost("Collect")]
        //[SuccessResultMessage("GenerateEntrySuccessfully")]
        public async Task Collect(long Id)
        {
            await _repository.GenerateEntryActions(Id, 2);
        }
        [HttpPost("Reject")]
        //[SuccessResultMessage("GenerateEntrySuccessfully")]
        public async Task Reject(long Id)
        {
            await _repository.GenerateEntryActions(Id, 3);
        }
        [HttpPost("CancelCollect")]
        public async Task CancelCollect(long Id)
        {
            await _repository.CancelEntryActions(Id, 4);
        }
        [HttpPost("CancelReject")]
        public async Task CancelReject(long Id)
        {
            await _repository.CancelEntryActions(Id, 5);
        }
    }
  



}
