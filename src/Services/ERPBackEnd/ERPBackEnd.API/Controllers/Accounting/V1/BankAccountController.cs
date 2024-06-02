using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ERPBackEnd.API.Helpers;
using Common.Repositories;
using AutoMapper;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.BankAccountes.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.BankAccounts.Dto;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class BankAccountController : MainController<BankAccount, BankAccountDto, long>
    {
        private IBankAccountRepository _repository;
        public BankAccountController(GMappRepository<BankAccount, BankAccountDto, long> mainRepos, IMapper mapper, IBankAccountRepository repository) : base(mainRepos)
        {
            _repository = repository;
        } 
        public override Task Delete(long id)
        {
            return _repository.DeleteAsync(id);
        }
        public override async Task<ActionResult<int>> Delete([FromBody] List<object> ids)
        {
            return await _repository.DeleteListAsync(ids);
        }
        public override async Task<BankAccountDto> GetById(long id)
        {
            return await _repository.FirstInclude(id);
        }
        public override async Task<BankAccountDto> Create([FromBody] BankAccountDto input)
        { 
            return await _repository.CreateEntity(input);
        }
        public override Task<BankAccountDto> Update([FromBody] BankAccountDto input)
        {
            return _repository.UpdateEntity(input);
        }
        public override async Task<ActionResult<PageList<BankAccountDto>>> GetAll([FromQuery] Paging paging)
        {
            return Ok(await _repository.GetAllIncluding(paging));
        }
    }
}
