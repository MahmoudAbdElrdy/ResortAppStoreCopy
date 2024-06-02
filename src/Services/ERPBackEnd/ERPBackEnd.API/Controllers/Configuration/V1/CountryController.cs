using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ResortAppStore.Services.ERPBackEnd.Application.Features.Countries.Dto;
using ERPBackEnd.API.Helpers;
using Common.Repositories;
using Configuration.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CountryController : MainController<Country, CountryDto, long>
    {

        public CountryController(GMappRepository<Country, CountryDto, long> mainRepos) : base(mainRepos)
        {

        }

    }
}
