using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ERPBackEnd.API.Helpers;
using Common.Repositories;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.AccountClassificationes.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountClassifications.Dto;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class AccountClassificationController : MainController<AccountClassification, AccountClassificationDto, long>
    {
        IAccountClassificationRepository _accountClassificationRepository;
       
        public AccountClassificationController(GMappRepository<AccountClassification, AccountClassificationDto, long> mainRepos, IAccountClassificationRepository accountClassificationRepository) : base(mainRepos)
        {
            _accountClassificationRepository = accountClassificationRepository; 
        }
        public override async Task<AccountClassificationDto> Update([FromBody] AccountClassificationDto input)
        {
            return await _accountClassificationRepository.Update(input);
        }
        public override Task<AccountClassificationDto> Create([FromBody] AccountClassificationDto input)
        {
            return _accountClassificationRepository.Create(input);
        }
    }
}
