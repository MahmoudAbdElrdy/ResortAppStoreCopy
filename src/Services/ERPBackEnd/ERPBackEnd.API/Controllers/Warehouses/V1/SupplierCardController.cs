
using Common.BaseController;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.SuppliersCard.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SupplierCardController : MainController<SupplierCard, SupplierCardDto, long>
    {
        public SupplierCardController(GMappRepository<SupplierCard, SupplierCardDto, long> mainRepos,
            ISupplierCardRepository SupplierCardRepository) : base(mainRepos)
        {
        }

       

       
    }
}
