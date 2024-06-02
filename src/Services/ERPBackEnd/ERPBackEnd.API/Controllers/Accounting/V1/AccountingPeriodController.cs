
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountingPeriodDto.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.AccountingPeriods.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Accounting.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class AccountingPeriodController : MainController<AccountingPeriod, AccountingPeriodDto, long>
    {
        public AccountingPeriodController(GMappRepository<AccountingPeriod, AccountingPeriodDto, long> mainRepos,
            IAccountingPeriodRepository accountingPeriodRepository) : base(mainRepos)
        {
        }

       

       
    }
}
