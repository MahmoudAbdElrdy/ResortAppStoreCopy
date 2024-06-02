using ERPBackEnd.API.Helpers;
using Common.BaseController;
using Common.Exceptions;
using Common.Infrastructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using Configuration.Entities;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class UserController : BaseController 
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
         //[AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost("forget-password")]
        
        public async Task<ActionResult<UserDto>> ForgetPasswordUserCommand([FromBody] ForgetPasswordUserCommand command)
        {

            return Ok((await _userRepository.ForgetPasswordUserCommand(command)));
        }
        [HttpPost("change-password")]
        
        public async Task<ActionResult<UserDto>> ChangePassword([FromBody] ChangePasswordCommand command) 
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
    }
}
