using AutoMapper;
using Common;
using Common.Enums;
using Common.Extensions;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Profiler;
using User = AuthDomain.Entities.Auth.User;

namespace ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Command
{
    public class TechnicalSupportCommand : IRequest<TechnicalSupportDto>
    {
        public string? UserName { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public UserType? UserType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
      //  public string[]? Roles { get; set; }
        public string? Password { get; set; }
        class Handler : IRequestHandler<TechnicalSupportCommand, TechnicalSupportDto>
        {
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IAuditService _auditService;
            private readonly IStringLocalizer<TechnicalSupportCommand> _stringLocalizer;
            private readonly string servicePath = "Identity\\Identity.Application\\Resources\\";
            public Handler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<TechnicalSupportCommand> stringLocalizer, IAuditService auditService)
            {
                _userManager = userManager;
                _mapper = mapper;
                _auditService = auditService;
                _stringLocalizer = stringLocalizer;
            }
            public async Task<TechnicalSupportDto> Handle(TechnicalSupportCommand request, CancellationToken cancellationToken)
            {
                var user = new User();
                //try
                //{

                var checkExsit = await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

                if (checkExsit != null)
                {
                    throw new UserFriendlyException(_stringLocalizer["EmailNumberFoundBefore", servicePath]);
                }
                //var checkExsitRegister = await _userManager.Users.Where(x => x.FullName == request.FullName && (x.IsVerifyCode == false || x.IsVerifyCode == null)).FirstOrDefaultAsync();
                //if (checkExsitRegister != null)
                //{
                //    await _userManager.DeleteAsync(checkExsitRegister);

                //}
                var phoneExsit = await _userManager.Users.Where(x => x.PhoneNumber == request.PhoneNumber).FirstOrDefaultAsync();

                if (phoneExsit != null)
                {
                    throw new UserFriendlyException(_stringLocalizer["NationalNumberFoundBefore", servicePath]);
                }

                user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                   
                    UserName = request.UserName,

                    Email = request.Email,

                  //  UserName = request.UserName,

                    PhoneNumber = request.PhoneNumber,

                    NormalizedEmail = request.Email,

                    NormalizedUserName = request.PhoneNumber,

                    CreatedOn = DateTime.Now,

                    IsDeleted = false,

                    UserType = request.UserType,

                    NameAr = request.NameAr,

                    NameEn = request.NameEn,

                    IsVerifyCode = false

                };
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {

                    throw new UserFriendlyException(_stringLocalizer["anErrorOccurredPleaseContactSystemAdministrator", servicePath]);
                }
                var roles = new List<string> { "SuperAdmin" };
                 await _userManager.AddToRolesAsync(user, roles);


                return _mapper.Map<TechnicalSupportDto>(user);
            }
        }
    }
}
