using AuthDomain.Entities.Auth;
using AutoMapper;
using Common;
using Common.Enums;
using Common.Extensions;
using Common.Interfaces;
using Common.Options;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Command
{
    public class LoginCommand : IRequest<AuthorizedUserDTO>
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        class Handler : IRequestHandler<LoginCommand, AuthorizedUserDTO>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Handler> _logger;
            private readonly UserManager<User> _userManager;
            private readonly IPasswordHasher<User> _passwordHasher;
            private readonly IConfiguration _configuration;
            private readonly JwtOption _jwtOption;
            private readonly IAuditService _auditService;
            private readonly IStringLocalizer<Handler> _localizationProvider;
            private readonly string servicePath = "Identity\\Identity.Application\\Resources\\";


            public Handler(
            IMapper mapper,
            ILogger<Handler> logger,
            UserManager<User> userManager,
            IPasswordHasher<User> passwordHasher,
            IConfiguration configuration,
            JwtOption jwtOption,
            IAuditService auditService,
            IStringLocalizer<Handler> localizationProvider)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
                _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
                _jwtOption = jwtOption;
                _auditService = auditService;
                _localizationProvider = localizationProvider;
            }
            public async Task<AuthorizedUserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var personalUser = await _userManager.Users.Where(x => x.FullName == request.UserName || x.PhoneNumber == request.UserName || x.Email == request.UserName).FirstOrDefaultAsync();

                if (personalUser == null)
                {
                    throw new UserFriendlyException("userNotFound");

                }


                else if (personalUser.IsDeleted)
                {
                    throw new UserFriendlyException("userAreDeleted");
                }


                var userHasValidPassword = await _userManager.CheckPasswordAsync(personalUser, request.PassWord);

                if (!userHasValidPassword)
                {
                    throw new UserFriendlyException("passWordNotCorrect");

                }
                var authorizedUserDto = new AuthorizedUserDTO
                {
                    User = _mapper.Map<TechnicalSupportDto>(personalUser),
                    Token = GenerateJSONWebToken(personalUser),
                };
                return authorizedUserDto;
            }
            private string GenerateJSONWebToken(User user)
            {

                var audience = _configuration["JwtOption:Audience"];

                var issuer = _configuration["JwtOption:Issuer"];

                var claims = (new List<Claim>() {
                    new Claim("userLoginId", user.Id.ToString()),
                    new Claim("phoneNumber", user.PhoneNumber),
                    new Claim("userName", user.UserName),
                    new Claim("fullName", user.FullName),
                    new Claim("userType", user.UserType.ToString())
                     });
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.Key));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtOption.ExpireDays));

                var tokenDescriptor = new JwtSecurityToken(
                  issuer,
                  audience,
                  claims,
                  expires: expires,
                  signingCredentials: cred
                );

                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
           
        }
    }
}
