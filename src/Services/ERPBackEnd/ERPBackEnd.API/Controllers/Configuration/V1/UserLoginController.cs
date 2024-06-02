
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
       
        private readonly IUserRepository _userRepository;
        public UserLoginController( IUserRepository userRepository)
        {
          
            _userRepository = userRepository;

        }
        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("Register")]
        //[SuccessResultMessage("AddedSuccessfully", "Identity\\Identity.Application\\Resources\\")]
        public async Task<UserLoginDto> Register([FromBody] UserLoginCommand command)
        {
            return await _userRepository.Register(command);
        }
        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("Login")]
        //  [SuccessResultMessage("AddedSuccessfully")]
        public async Task<AuthorizedUserDTO> Longin([FromBody] LoginCommand command)
        {
            return await _userRepository.LoginCommand(command);
        } 
        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("LoginCompany")]
        //  [SuccessResultMessage("AddedSuccessfully")]
        public async Task<AuthorizedUserDTO> LoginCompanyCommand([FromBody] LoginCompanyCommand command)
        {
            return await _userRepository.LoginCompanyCommand(command);
        }
        [AllowAnonymous]
        [HttpGet("get-ddl")]
        public async Task<ActionResult<List<CompanyDto>>> All()
        {
            return Ok((await _userRepository.GetAllCompanyQuery()));
        } 
        [AllowAnonymous]
        [HttpGet("companies-organizationId")]
        public async Task<ActionResult<List<CompanyDto>>> All([FromQuery] long organizationId)
        {
            return Ok((await _userRepository.GetAllCompanyByOrganization(organizationId)));
        }
        [AllowAnonymous]
        [HttpGet("get-ddlWithCompanies")]
        public async Task<ActionResult<List<BranchDto>>> All([FromQuery] GetAllBranchesQuery query)
        {
            return Ok((await _userRepository.GetAllBranches(query)));
        }
        [AllowAnonymous]
        [HttpGet("getUserBranches")]
        public async Task<ActionResult<List<BranchDto>>> GetUserBranches([FromQuery] string userId, long companyId)
        {
            return Ok((await _userRepository.GetBranchesByUserId(userId,companyId)));
        }
    }
}