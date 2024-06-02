
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomerGroups.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomerGroups.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CustomerGroupController : MainController<CustomerGroup, CustomerGroupDto, long>
    {
        public CustomerGroupController(GMappRepository<CustomerGroup, CustomerGroupDto, long> mainRepos,
            ICustomerGroupRepository CustomerGroupRepository) : base(mainRepos)
        {
        }

       

       
    }
}
