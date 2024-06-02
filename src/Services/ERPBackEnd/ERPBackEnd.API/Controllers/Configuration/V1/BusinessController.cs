using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Businesss.Dto;
using Common.Repositories;
using Configuration.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class BusinessController : MainController<Business, BusinessDto, long>
    {

        public BusinessController(GMappRepository<Business, BusinessDto, long> mainRepos) : base(mainRepos)
        {

        }

    }
   
}
