using Administration.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.Administration.Application.Auth.Payment.Dto;
using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Dto;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserOwnerRepository _userOwnerRepository;
        public UserController(IUserRepository userRepository, IUserOwnerRepository  userOwnerRepository)
        {
            _userRepository = userRepository;
            _userOwnerRepository = userOwnerRepository;
        }
        // [AllowAnonymous]
        [HttpGet("get-ddl")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return Ok((await _userRepository.GetAllUserQuery()));
        }

        // [AllowAnonymous]
        [HttpGet("all")]
        public async Task<ActionResult<PageList<UserDto>>> Show([FromQuery] GetAllUsersWithPaginationCommand query)
        {
            return Ok((await _userRepository.GetAllUsersWithPaginationCommand(query)));
        }
        //// [AllowAnonymous]
        [HttpPost("add")]
        [SuccessResultMessage("createSuccessfully")]
        public async Task<ActionResult<UserDto>> Add([FromBody] CreateUserCommand command)
        {
            return Ok((await _userRepository.CreateUserCommand(command)));
        }
        // [AllowAnonymous]
        [HttpPost("edit")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<UserDto>> Edit([FromBody] EditUserCommand command)
        {

            return Ok((await _userRepository.EditUserCommand(command)));
        }
        [AllowAnonymous]
        [HttpPost("add-password")]

        public async Task<ActionResult<UserDto>> AddPasswordUserCommand([FromBody] AddPasswordUserCommand command)
        {

            return Ok((await _userRepository.AddPasswordUserCommand(command)));
        }


        [HttpPost("create-userOrganization")]
        public async Task<ActionResult<long>> CreateUserOrganization([FromBody] OrganizationDto command)
        {
            return Ok((await _userRepository.CreateUserOrganizations(command)));
        }


        [HttpGet("get-userOrganizations/{userName}")]
        public async Task<ActionResult<List<OrganizationDto>>> GetUserOrganization(string userName)
        {
            return Ok((await _userRepository.GetUserOrganizations(userName)));
        }

        [AllowAnonymous]
        [HttpPost("forget-password")]

        public async Task<ActionResult<UserDto>> ForgetPasswordUserCommand([FromBody] ForgetPasswordUserCommand command)
        {

            return Ok((await _userRepository.ForgetPasswordUserCommand(command)));
        }
        [HttpPost("change-password")]

        public async Task<ActionResult<UserDto>> ChangePasswordCommand([FromBody] ChangePasswordCommand command)
        {

            return Ok((await _userRepository.ChangePasswordCommand(command)));
        }
        // [AllowAnonymous]
        [HttpGet("delete")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<UserDto>> Delete(string id)
        {
            return Ok(await _userRepository.DeleteUserCommand(new DeleteUserCommand() { Id = id }));
        }
        // [AllowAnonymous]
        [HttpGet("getById")]

        public async Task<ActionResult<UserDto>> GetById(string id)
        {
            return Ok(await _userRepository.GetById(new GetById() { Id = id }));
        }  
      
        [HttpGet("getUserOwner")]

        public async Task<ActionResult<UserOwnerDto>> GetById([FromQuery] GetById request)
        {
            return Ok(await _userOwnerRepository.GetUserOwner(request));
        }
        [HttpGet("getUserDetailsPackageById")]

        public async Task<ActionResult<UserDetailsPackageDto>> GetById([FromQuery] GetUserDetailsPackageById request)
        {
            return Ok(await _userOwnerRepository.GetUserDetailsPackage(request));
        }

        [HttpGet("getUserDetailsModuleByCode")]

        public async Task<ActionResult<List<ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto.UserDetailsModuleDto>>> GetById([FromQuery] GetUserDetailsModuleByCode request)
        {
            return Ok(await _userOwnerRepository.GetUserDetailsModule(request));
        }

        [HttpPost("AddOwnerCompanies")]
        public async Task<ActionResult<long>> AddOwnerCompanies([FromBody] UserCompaniesDto request)
        {
            return Ok(await _userRepository.CreateUserCompanies(request));
        }

        [HttpGet("GetCompanies")]
        public async Task<ActionResult<OrganizationDto>> GetCompanies([FromQuery]GetUserCompanyRequest request)
        {
            return Ok(await _userRepository.GetUserCompanies(request));
        }


        [HttpPost("AddOwnerPackages")]
        public async Task<ActionResult<CreateUserDetailsPackageCommand>> AddOwnerPackages([FromBody] CreateUserDetailsPackageCommand request)
        {
            return Ok(await _userRepository.CreateUserPackageDetails(request));
        }


        [HttpPost("AddOwnerCustomModules")]
        public async Task<ActionResult<List<CreateUserDetailsModulesCommand>>> AddOwnerCustomModules([FromBody] List<CreateUserDetailsModulesCommand> request)
        {
            return Ok(await _userRepository.CreateUserDetailsModules(request));
        }


        // [AllowAnonymous]
        [HttpGet("getLastCode")]

        public async Task<ActionResult<string>> GetLastCode()
        {
            return Ok(await _userRepository.GetLastCode());
        }
        [HttpPost("deleteList")]
        [SuccessResultMessage("deleteSuccessfully")]
        public async Task<ActionResult<UserDto>> Delete([FromBody] DeleteListUserCommand command)
        {
            return Ok(await _userRepository.DeleteListUserCommand(command));
        }
        [HttpPost("paid-payment")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult<bool>> Edit([FromBody] EditUserPaymentDto command)
        {

            return Ok((await _userOwnerRepository.EditUserPayment(command)));
        }
      
        [HttpPost("AddTrial")]
        [SuccessResultMessage("editSuccessfully")]
        public async Task<ActionResult> AddTrial([FromBody] TrialDto command)
        {

            return Ok((await _userRepository.AddTrialCommand(command)));
        }


        [HttpGet("GetPaymentRequestLink/{id}")]
        public async Task<ActionResult<PaymentResponseLinkDto>> GetPaymentRequestLink([FromRoute] long id)
        {
            var result = await _userRepository.GetPaymentRequestLinkDto(id);
            return Ok(result);
        }



    }
}
