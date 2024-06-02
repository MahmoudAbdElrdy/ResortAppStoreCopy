using Common.BaseController;
using Common.Repositories;
using Configuration.Entities;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.BeneficiariesDto.Dto;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Configuration.V1
{
  
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class NotificationsManagementController : MainController<Beneficiaries, BeneficiariesDto, long>
    {
        public NotificationsManagementController(GMappRepository<Beneficiaries, BeneficiariesDto, long> mainRepos) : base(mainRepos)
        {
        }
    }
}
