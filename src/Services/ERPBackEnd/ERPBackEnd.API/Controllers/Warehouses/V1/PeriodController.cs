
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Periods.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Periods.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class PeriodController : MainController<Period, PeriodDto, long>
    {
        public PeriodController(GMappRepository<Period, PeriodDto, long> mainRepos,
            IPeriodRepository PeriodRepository) : base(mainRepos)
        {
        }

       

       
    }
}
