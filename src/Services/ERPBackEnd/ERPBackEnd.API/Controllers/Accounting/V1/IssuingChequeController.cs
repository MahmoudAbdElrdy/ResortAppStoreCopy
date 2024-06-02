using AutoMapper;
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class IssuingChequeController : MainController<IssuingChequeMaster, IssuingChequeMasterDto, long>
    {
        private IIssuingChequeRepository _repository;

        public IssuingChequeController(
            GMappRepository<IssuingChequeMaster, IssuingChequeMasterDto, long> mainRepos, IMapper mapper,
            IIssuingChequeRepository repository) : base(mainRepos)
        {
            _repository = repository;

        }
        public override async Task<IssuingChequeMasterDto> Create([FromBody] IssuingChequeMasterDto input)
        {
            return await _repository.CreateIssuingCheque(input);
        }

        public override async Task<IssuingChequeMasterDto> Update([FromBody] IssuingChequeMasterDto input)
        {
            return await _repository.UpdateIssuingCheque(input);
        }
        public override async Task<IssuingChequeMasterDto> GetById(long id)
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
