using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SalesPersonCommissionController : MainController<SalesPersonCommission, SalesPersonCommissionDto, long>
    {
        public SalesPersonCommissionController(GMappRepository<SalesPersonCommission, SalesPersonCommissionDto, long> mainRepos,
            ISalesPersonCommissionRepository salesPersonCommissionRepository) : base(mainRepos)
        {
        }
    }
}
