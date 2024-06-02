
using Common.BaseController;
using Common.Infrastructures;
using Common.Repositories;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.CustomersCard.Repository;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Warehouses.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CustomerCardController : MainController<CustomerCard, CustomerCardDto, long>
    {
        private ICustomerCardRepository _customerRepo;
        public CustomerCardController(GMappRepository<CustomerCard, CustomerCardDto, long> mainRepos, ICustomerCardRepository  customerRepo) : 
            base(mainRepos)
        {
            _customerRepo = customerRepo;
        }


        [HttpGet]
        [Route("AllCardList")]
        public virtual async Task<ActionResult<PaginatedList<CustomerCardDto>>> GetAllCardList([FromQuery] Paging paging)
        {

            return Ok(await _customerRepo.GetAllCardList(paging));

        }



    }
}
