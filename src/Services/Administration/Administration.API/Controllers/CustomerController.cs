using Microsoft.AspNetCore.Mvc;
using Common.BaseController;
using Common.Infrastructures;
using Common.Exceptions;
using Administration.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using ResortAppStore.Services.Administration.Application.Features.Customers.Repository;
using ResortAppStore.Services.Administration.Application.Features.Customers.Dto;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class CustomerController : BaseController
    {
        private ICustomerRepository _customerRepository; 
        public CustomerController(ICustomerRepository customerRepository) 
        {
            _customerRepository = customerRepository;
        }
        //[AllowAnonymous]
        [HttpGet("get-ddl")]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            return Ok((await _customerRepository.GetAllCustomersQuery()));
        }

        //[AllowAnonymous]
        [HttpGet("all")] 
        public async Task<ActionResult<PageList<CustomerDto>>> Show([FromQuery] GetAllCustomersWithPaginationCommand query)
        {
            return Ok((await _customerRepository.GetAllCustomersWithPaginationCommand(query)));
        }
       //// [AllowAnonymous]
        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<CustomerDto>> Add([FromBody] CreateCustomerCommand command)
        {
            return Ok((await _customerRepository.CreateCustomerCommand(command)));
        }
        //[AllowAnonymous]
        [HttpPost("edit")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<CustomerDto>> Edit([FromBody] EditCustomerCommand command )
        {
           
            return Ok((await _customerRepository.EditCustomerCommand(command)));
        }
        [AllowAnonymous]
        [HttpPost("verify-code")]
     //  [SuccessResultMessage("VerifyCodeSuccessfully")]
        public async Task<ActionResult<string>> VerifyCodeCommand([FromBody] VerifyCodeCommand command )
        {
           
            return Ok((await _customerRepository.VerifyCodeCommand(command)));
        }
        //[AllowAnonymous]
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<CustomerDto>> Delete(long id)
        {
            return Ok(await _customerRepository.DeleteCustomerCommand(new DeleteCustomerCommand() { Id = id }));
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<int>> Delete([FromBody] DeleteListCustomerCommand command)
        {
            return Ok(await _customerRepository.DeleteListCustomerCommand(command));
        }
       
        [HttpGet("getById")]
        
        public async Task<ActionResult<CustomerDto>> GetById(long id) 
        {
            return Ok(await _customerRepository.GetByCustomerId(new GetByCustomerId() { Id = id }));
        }


        [AllowAnonymous]
        [HttpGet("GetCustomer")]
        
        public async Task<ActionResult<CustomerDto>> GetCustomer(string subDomain) 
        {
            return Ok(await _customerRepository.GetCustomer(new CustomerSubDomain() { SubDomain = subDomain }));
        }   
        //[AllowAnonymous]
        [HttpGet("getLastCode")]
        
        public async Task<ActionResult<string>> GetLastCode() 
        {
            return Ok(await _customerRepository.GetLastCode());
        }
        //// [AllowAnonymous]
        [HttpPost("addSubscription")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<CustomerSubscriptionDto>> Add([FromBody] CreateCustomerSubscriptionCommand command)
        {
            return Ok((await _customerRepository.CreateCustomerSubscriptionCommand(command)));
        }
        //[AllowAnonymous]
        [HttpPost("editSubscription")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<CustomerSubscriptionDto>> Edit([FromBody] EditCustomerSubscriptionCommand command)
        {

            return Ok((await _customerRepository.EditCustomerSubscriptionCommand(command)));
        }
        [HttpGet("deleteSubscription")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<CustomerDto>> DeleteSubscription(long id)
        {
            return Ok(await _customerRepository.DeleteCustomerSubscriptionCommand(new DeleteCustomerSubscriptionCommand() { Id = id }));
        }
        [HttpPost("deleteListSubscription")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<int>> DeleteSubscription([FromBody] DeleteListCustomerSubscriptionCommand command)
        {
            return Ok(await _customerRepository.DeleteListCustomerSubscriptionCommand(command));
        }
        [HttpGet("getByIdSubscription")]
        public async Task<ActionResult<CustomerSubscriptionDto>> GetByCustomerSubscriptionId(long id)
        {
            return Ok(await _customerRepository.GetByCustomerSubscriptionId(new GetByCustomerSubscriptionId() { Id = id }));
        }
    }
}
