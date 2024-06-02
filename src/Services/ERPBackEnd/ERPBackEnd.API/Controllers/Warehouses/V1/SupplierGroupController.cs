
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SupplierGroups.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SupplierGroups.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SupplierGroupController : MainController<SupplierGroup, SupplierGroupDto, long>
    {
        public SupplierGroupController(GMappRepository<SupplierGroup, SupplierGroupDto, long> mainRepos,
            ISupplierGroupRepository SupplierGroupRepository) : base(mainRepos)
        {
        }

       

       
    }
}
