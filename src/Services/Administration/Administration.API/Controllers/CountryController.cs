using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using ResortAppStore.Services.Administration.Application.Features.Countries.Dto;
using Administration.API.Helpers;
using Common.Repositories;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;

namespace ResortAppStore.Services.Administration.API.Controllers
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
