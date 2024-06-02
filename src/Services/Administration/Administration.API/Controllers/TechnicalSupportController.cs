using AuthDomain.Entities.Auth;
using Common;
using Common.BaseController;
using Common.Exceptions;
using Common.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository;
using AuthorizedUserDTO = ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto.AuthorizedUserDTO;

namespace ResortAppStore.Services.Administration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicalSupportController : MainController<User, UserDto, string>
    {
        private readonly IUserRepository _userRepository;
        public TechnicalSupportController(IUserRepository userRepository, GMappRepository<User, UserDto, string> mainRepos):base(mainRepos)
        {
            _userRepository = userRepository;
        }
        //private readonly IMediator _mediator;

        //public TechnicalSupportController(IMediator mediator)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //}
        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("Register")]
        //[SuccessResultMessage("AddedSuccessfully", "Identity\\Identity.Application\\Resources\\")]
        public async Task<TechnicalSupportDto> Register([FromBody] TechnicalSupportCommand command)
        {
            return await _userRepository.Register(command);
        }

        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("RegisterOwner")]
        //[SuccessResultMessage("AddedSuccessfully", "Identity\\Identity.Application\\Resources\\")]
        public async Task<AuthorizedUserDTO> RegisterOwner([FromBody] TechnicalSupportCommand command)
        {
            return await _userRepository.RegisterOwner(command);
        }

        [HttpPost]
        [AllowAnonymous]
        //AddedSuccessfully
        [Route("Login")]
        //[SuccessResultMessage("AddedSuccessfully")]
        public async Task<AuthorizedUserDTO> Longin([FromBody] LoginCommand command)
        {
            return await _userRepository.LoginCommand(command);
        }
       
       
       
        [HttpPost("logout")]
        public async Task<bool> LogOut()
        {
            return await _userRepository.LogOut();
        }
      
        [HttpPost("checkSession")]
        public async Task<bool> CheckSession()
        {
            return await _userRepository.CheckSession();
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("LonginWithGoogle")]
        //[SuccessResultMessage("AddedSuccessfully")]
        public async Task<AuthorizedUserDTO> LonginWithGoogle([FromBody] string command)
        {
            return await _userRepository.LoginWithGoogleCommand(command);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SignUpWithGoogle")]
        //[SuccessResultMessage("AddedSuccessfully")]
        public async Task<UserFbInfo> SignUpWithGoogle([FromBody] string command)
        {
            return await _userRepository.SignUpWithGoogleCommand(command);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("LonginWithFacebook")]
        //[SuccessResultMessage("AddedSuccessfully")]
        public async Task<AuthorizedUserDTO> LonginWithFacebook([FromBody] string command)
        {
            return await _userRepository.LoginWithFacebookCommand(command);
        }
    }
}
