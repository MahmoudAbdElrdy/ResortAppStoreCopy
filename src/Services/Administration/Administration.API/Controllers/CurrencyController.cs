using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using Administration.API.Helpers;
using Common.Repositories;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;
using ResortAppStore.Services.Administration.Application.Features.Currencies.Dto;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CurrencyController : MainController<Currency, CurrencyDto, long>
    {

        public CurrencyController(GMappRepository<Currency, CurrencyDto, long> mainRepos) : base(mainRepos)
        {

        }

    }
}
