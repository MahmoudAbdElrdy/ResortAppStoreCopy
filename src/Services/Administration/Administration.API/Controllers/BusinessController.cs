using Administration.API.Helpers;
using Common.BaseController;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Businesss.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;

namespace ResortAppStore.Services.Administration.API.Controllers
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
