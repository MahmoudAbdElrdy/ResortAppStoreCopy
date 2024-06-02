using AuthDomain.Entities.Auth;
using AutoMapper;
using Azure;
using Common.Constants;
using Common.Enums;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Options;
using Common.SharedDto;
using CRM.Services.Helpers;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Nashmi.Services.NPay.Data.ExternalServices.PaymentApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.Payment.Dto;
using ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsModules.Command;
using ResortAppStore.Services.Administration.Application.Auth.UserDetailsPackages.Command;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthorizedUserDTO = ResortAppStore.Services.Administration.Application.Auth.TechnicalSupport.Dto.AuthorizedUserDTO;
using Response = Common.Constants.Response;
using User = AuthDomain.Entities.Auth.User;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtOption _jwtOption;
        private readonly IEmailHelper _emailHleper;
        private readonly IStringLocalizer<UserLoginCommand> _stringLocalizer;
        private readonly string servicePath = "Identity\\Identity.Application\\Resources\\";
        private readonly IGRepository<User> _contextUser;
        private readonly IAuditService _auditService;
        private readonly IGRepository<Role> _roleService;
        private readonly IGRepository<UserRole> _userRole;
        private readonly IGRepository<UserDetailsPackage> _userDetailsPackage;
        private readonly IGRepository<UserDetailsModule> _userDetailsModule;
        private readonly IGRepository<UserDetailsPackagesModules> _packageModulesContext;
        private readonly IGRepository<Module> _modulesContext;
        private readonly IGRepository<PackagesModules> _modulesPackagesContext;
        private readonly HttpClient _httpClient;
        private readonly IGRepository<OrganizationCompany> _userCompanies;
        private readonly IGRepository<UserOrganization> _userOrganization;
        private readonly IGRepository<UserSubscriptionPromoCode> _userPromoCode;
        private readonly IGRepository<UserDetailsModule> _userDetailsModuleCtx;
        private readonly IGRepository<UserOrgnizationType> _userOrgnizationType;
        private readonly IGRepository<UserPayment> _userPayment;
        private readonly IGRepository<UserToken> _userToken;
        private readonly ISettingErpApi _api;
        private readonly IGRepository<PackageModuleCompany> _packageModuleCompany;
        private readonly IGRepository<UserPaymentOnline> _userPaymentOnline;
        public UserRepository(
        UserManager<User> userManager,
        IGRepository<UserOrganization> userOrganization,
        IConfiguration configuration,
        JwtOption jwtOption,
        IEmailHelper emailHleper,
        IStringLocalizer<UserLoginCommand> stringLocalizer,
        IGRepository<User> contextUser,
        IAuditService auditService,
        IGRepository<Role> roleService,
        IGRepository<UserRole> userRole,
        HttpClient httpClient,
        IGRepository<UserDetailsPackage> userDetailsPackage,
        IGRepository<UserDetailsModule> userDetailsModule,
        IGRepository<Module> modulesContext,
        IGRepository<PackagesModules> modulesPackagesContext,
        IGRepository<OrganizationCompany> userCompanies,
        IGRepository<UserSubscriptionPromoCode> userPromoCode,
        IGRepository<UserDetailsModule> userDetailsModuleCtx,
        IGRepository<UserOrgnizationType> userOrgnizationType,
        IGRepository<PackageModuleCompany> packageModuleCompany,
        IGRepository<UserPayment> userPayment,
        IGRepository<UserToken> userToken,
        IGRepository<UserPaymentOnline> userPaymentOnline,
        IMapper mapper, IGRepository<UserDetailsPackagesModules> packageModulesContext, ISettingErpApi api)
        {

            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtOption = jwtOption;
            _emailHleper = emailHleper;
            _stringLocalizer = stringLocalizer;
            _contextUser = contextUser;
            _auditService = auditService;
            _roleService = roleService;
            _userRole = userRole;
            _httpClient = httpClient;
            _userDetailsModule = userDetailsModule;
            _userDetailsPackage = userDetailsPackage;
            _packageModulesContext = packageModulesContext;
            _modulesContext = modulesContext;
            _modulesPackagesContext = modulesPackagesContext;
            _userCompanies = userCompanies;
            _userPromoCode = userPromoCode;
            _userDetailsModuleCtx = userDetailsModuleCtx;
            _userOrgnizationType = userOrgnizationType;
            _userPayment = userPayment;
            _api = api;
            _packageModuleCompany = packageModuleCompany;
            _userOrganization = userOrganization;
            _userToken = userToken;
            _userPaymentOnline = userPaymentOnline;
        }

        public async Task<bool> LogOut()
        {
            var personalUser = await _userManager.Users.Where(x => x.Id == _auditService.UserId).FirstOrDefaultAsync();

            if (personalUser == null)
            {
                throw new UserFriendlyException("userNotFound");

            }
            else if (personalUser.IsDeleted)
            {
                throw new UserFriendlyException("userAreDeleted");
            }
            // Check for existing active sessions
            var activeSession = await _userToken.GetAll()
                .Where(t => t.UserId == personalUser.Id && t.IsValid)
                .FirstOrDefaultAsync();
            if (activeSession != null)
            {
                activeSession.IsValid = false;
                activeSession.LastActivity = DateTime.Now.AddMinutes(-1);
                _userToken.Update(activeSession);
                await _userToken.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> CheckSession()
        {
            var personalUser = await _userManager.Users.Where(x => x.Id == _auditService.UserId).FirstOrDefaultAsync();

            if (personalUser == null)
            {
                throw new UserFriendlyException("userNotFound");

            }

            else if (personalUser.IsDeleted)
            {
                throw new UserFriendlyException("userAreDeleted");
            }
            // Check for existing active sessions
            var activeSession = await _userToken.GetAll()
                .Where(t => t.UserId == personalUser.Id && t.IsValid)
                .FirstOrDefaultAsync();


            if (activeSession != null)
            {
                activeSession.IsValid = true;
                activeSession.LastActivity = DateTime.Now;
                _userToken.Update(activeSession);
                await _userToken.SaveChangesAsync();
            }
            return true;
        }
        public async Task<AuthorizedUserDTO> LoginCommand(LoginCommand request)
        {
            var personalUser = await _userManager.Users.Where(x => x.FullName == request.UserName || x.PhoneNumber == request.UserName || x.Email == request.UserName || x.UserName == request.UserName).FirstOrDefaultAsync();

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

            // Check for existing active sessions
            var activeSession = await _userToken.GetAll()
                .Where(t => t.UserId == personalUser.Id && t.IsValid)
                .FirstOrDefaultAsync();

            // If an active session exists, invalidate it
            if (activeSession != null && activeSession.LastActivity.HasValue && (DateTime.Now - activeSession.LastActivity.Value).TotalMinutes < 1.5)
            {
                //activeSession.IsValid = false;
                //await _userToken.UpdateAsync(activeSession);
                //await _userToken.SaveChangesAsync();
                throw new UserFriendlyException("userAlreadyLoggedIn");
            }
            var token = GenerateJSONWebToken(personalUser);
            // Create a new session
            var userSession = new UserToken
            {
                UserId = personalUser.Id,
                Token = token,
                Expiry = DateTime.Now.AddDays(Convert.ToDouble(_jwtOption.ExpireDays)),
                IsValid = true
            };
            _userToken.Insert(userSession);
            await _userToken.SaveChangesAsync();
            var authorizedUserDto = new AuthorizedUserDTO
            {
                User = _mapper.Map<TechnicalSupportDto>(personalUser),
                Token = token,
            };

            var moduleUserDetail = _userDetailsModule.GetAll().Where(x => x.UserId == personalUser.Id).ToList();
            var packageUserDetail = _userDetailsPackage.GetAll().Where(x => x.UserId == personalUser.Id).ToList();
            if (moduleUserDetail.Count > 0 || packageUserDetail.Count > 0)
            {
                authorizedUserDto.User.IsSubscriped = true;
            }
            else
            {
                authorizedUserDto.User.IsSubscriped = false;
            }
            var loginOwner = new AuthorizedUserOwnerDTO()
            {
                Token = token,
                User = new OwnerLoginDto()
                {
                    Email = personalUser.Email,
                    IsSubscriped = authorizedUserDto.User.IsSubscriped,
                    NameAr = personalUser.NameAr,
                    NameEn = personalUser.NameEn,
                    PhoneNumber = personalUser.PhoneNumber,
                    UserId = personalUser.Id,
                    UserName = personalUser.UserName,
                    UserType = authorizedUserDto.User.UserType,

                }
            };
            //var res=  await  this._api.LoginOwner(loginOwner);
            return authorizedUserDto;
        }

        public async Task<AuthorizedUserDTO> LoginWithGoogleCommand(string request)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ClientId"] }

            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request, settings);

            var user = _userManager.Users.Where(x => x.Email == payload.Email).FirstOrDefault();

            if (user != null)
            {
                var authorizedUserDto = new AuthorizedUserDTO
                {
                    User = _mapper.Map<TechnicalSupportDto>(user),
                    Token = GenerateJSONWebToken(user),
                };
                return authorizedUserDto;
            }
            else
            {
                throw new UserFriendlyException("userIsNotExistPleaseSingUp");

            }
        }


        public async Task<UserFbInfo> SignUpWithGoogleCommand(string request)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ClientId"] }

            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request, settings);

            var user = _userManager.Users.Where(x => x.Email == payload.Email).FirstOrDefault();

            if (user == null)
            {
                return new UserFbInfo
                {
                    Email = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                };

            }

            else
            {
                throw new UserFriendlyException("userIsAlreadyExistPleaseLogin");
            }
        }


        public async Task<AuthorizedUserDTO> LoginWithFacebookCommand(string request)
        {
            long facebookId = Int64.Parse(_configuration["FacebookId"].ToString());
            string facebookSecret = _configuration["FacebookSecret"];
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("https://graph.facebook.com/debug_token?input_token=" + request + $"access_token={facebookId}|{facebookSecret}");

            var stringThings = await httpResponseMessage.Content.ReadAsStringAsync();
            var userObject = JsonConvert.DeserializeObject<FBUser>(stringThings);

            if (userObject.Data.IsValid == false)
            {
                throw new UserFriendlyException("unAthuorized");

            }

            HttpResponseMessage responseMessage = await _httpClient.GetAsync("https://graph.facebook.com/me?fields=first_name,last_name.email,id&access_token=" + request);
            var userContent = await responseMessage.Content.ReadAsStringAsync();
            var userContentObj = JsonConvert.DeserializeObject<UserFbInfo>(userContent);

            var user = _userManager.Users.Where(x => x.Email == userContentObj.Email).FirstOrDefault();

            if (user != null)
            {
                var authorizedUserDto = new AuthorizedUserDTO
                {
                    User = _mapper.Map<TechnicalSupportDto>(user),
                    Token = GenerateJSONWebToken(user),
                };
                return authorizedUserDto;
            }
            else
            {
                throw new UserFriendlyException("userNotFound");
            }


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

        public async Task<TechnicalSupportDto> Register(TechnicalSupportCommand request)
        {
            var user = new User();

            var checkExsit = await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

            if (checkExsit != null)
            {
                throw new UserFriendlyException(_stringLocalizer["EmailNumberFoundBefore", servicePath]);
            }

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

                FullName = request.NameAr,

                PhoneNumber = "",

                NormalizedEmail = request.Email.ToUpper(),

                NormalizedUserName = request.UserName.ToUpper(),

                CreatedOn = DateTime.Now,

                IsDeleted = false,

                UserType = request.UserType,

                NameAr = request.NameAr,

                NameEn = request.NameEn,

                IsVerifyCode = false,

                IsActive = true

            };

            var result = await _userManager.CreateAsync(user, (request.Password != "" ? request.Password : "123456"));
            if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateEmail")
            {
                // await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("duplicateEmail");
            }
            else if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateUserName")
            {
                //  await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("duplicateUserName");
            }
            else if (!result.Succeeded)
            {
                // await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("invalidUsername");
            }
            if (request.UserType == UserType.Technical)
            {
                try
                {

                    var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");
                    string verificationLink = $"{applicationUrl}/authentication/add-password?email={user.Email}";
                    string message = EmailTemplates.AddPassword(user.FullName, verificationLink);
                    _emailHleper.SendEmailAsync(user.Email, message, "اضافة كلمة المرور");
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("probleminSenMail");
                }
            }
            if (request.UserType == UserType.Owner)
            {

                var roles = new List<string> { "SuperAdmin" };
                await _userManager.AddToRolesAsync(user, roles);
            }


            return _mapper.Map<TechnicalSupportDto>(user);
        }
        public async Task<List<UserDto>> GetAllUserQuery()
        {
            var UserList = await _contextUser.GetAllListAsync(x => !x.IsDeleted);

            return _mapper.Map<List<UserDto>>(UserList);

        }
        public async Task<PaginatedList<UserDto>> GetAllUsersWithPaginationCommand(GetAllUsersWithPaginationCommand request)
        {
            var query = _contextUser.GetAllIncluding(x => x.UserRoles).Where(c => !c.IsDeleted && c.UserType == request.UserType);

            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.NameAr.Contains(request.Filter));
            }

            var entities = query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize).OrderBy(c => c.Code).ToList();

            var totalCount = await query.CountAsync();

            var transferReasonDto = _mapper.Map<List<UserDto>>(entities);

            return new PaginatedList<UserDto>(transferReasonDto,
                totalCount,
                request.PageIndex,
                request.PageSize);

        }
        public async Task<UserDto> CreateUserCommand(CreateUserCommand request)
        {
            var user = new User();
            if (request.Code == _auditService.Code)
            {
                var lastCode = await _userManager.Users.OrderByDescending(c => c.Id).FirstOrDefaultAsync(x => !x.IsDeleted);

                var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));

                if (request.Code != code)
                {
                    var existCode = await _userManager.Users.AnyAsync(x => x.Code == request.Code && !x.IsDeleted);

                    request.Code = code;
                }
            }
            else
            {
                var existCode = await _userManager.Users.AnyAsync(x => x.Code == request.Code && !x.IsDeleted);
                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }


            var checkExsit = await _userManager.Users.Where(x => x.Email == request.Email && !x.IsDeleted).FirstOrDefaultAsync();

            if (checkExsit != null)
            {
                throw new UserFriendlyException("emailNumberFoundBefore");
            }

            var phoneExsit = await _userManager.Users.Where(x => x.PhoneNumber == request.PhoneNumber && !x.IsDeleted).FirstOrDefaultAsync();

            if (phoneExsit != null)
            {
                throw new UserFriendlyException("NationalNumberFoundBefore");
            }


            user = new User()
            {
                Id = Guid.NewGuid().ToString(),

                Email = request.Email,

                FullName = request.FullName,

                PhoneNumber = request.PhoneNumber,

                NormalizedEmail = request.Email,

                CreatedBy = _auditService.UserName,

                IsActive = (bool)request.IsActive,

                CreatedAt = DateTime.UtcNow,

                UserType = UserType.Technical,

                NameAr = request.NameAr,

                NameEn = request.NameEn,

                IsVerifyCode = false,

                Code = request.Code,


                IsAddPassword = false,

            };
            //  NormalizedUserName = user.Id.ToUpper(),
            user.UserName = user.Id;
            user.NormalizedUserName = user.Id.ToUpper();

            var result = await _userManager.CreateAsync(user, "123456");

            if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateEmail")
            {
                // await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("duplicateEmail");
            }
            else if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateUserName")
            {
                //  await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("duplicateUserName");
            }
            else if (!result.Succeeded)
            {
                // await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("anErrorOccurredPleaseContactSystemAdministrator");
            }
            // var roles = request.Roles;
            try
            {
                var roles = await _roleService.GetAllListAsync(x => request.Roles.Contains(x.Id));
                if (roles != null && roles.Count > 0)
                    await _userManager.AddToRolesAsync(user, roles.Select(c => c.Name));

            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("errorInRole");
            }
            try
            {

                var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");

                string verificationLink = $"{applicationUrl}/authentication/add-password?email={user.Email}";
                string message = EmailTemplates.AddPassword(user.FullName, verificationLink);
                _emailHleper.SendEmailAsync(user.Email, message, "اضافة كلمة المرور");
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("probleminSenMail");
            }


            return _mapper.Map<UserDto>(user);


        }
        public async Task<UserDto> EditUserCommand(EditUserCommand request)
        {
            var user = new User();

            user = await _userManager.Users.Where(x => x.Id == request.Id && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("emailUserNotFound");
            }

            var existCode = await _userManager.Users.AnyAsync(x => x.Id != user.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");


            user.UserName = request.Id;

            user.FullName = request.FullName;

            user.Email = request.Email;

            user.PhoneNumber = request.PhoneNumber;


            user.IsActive = (bool)request.IsActive;

            user.UpdatedAt = DateTime.UtcNow;

            user.NameAr = request.NameAr;

            user.NameEn = request.NameEn;

            user.Code = request.Code;



            var roles = await _userManager.GetRolesAsync(user);

            var resultRole = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!resultRole.Succeeded)
                throw new UserFriendlyException("errorInRole");


            var rolesDb = await _roleService.GetAllListAsync(x => request.Roles.Contains(x.Id));

            if (rolesDb != null && rolesDb.Count > 0)
                await _userManager.AddToRolesAsync(user, rolesDb.Select(c => c.Name));

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateEmail")
            {
                throw new UserFriendlyException("duplicateEmail");
            }
            else if (!result.Succeeded)
            {

                throw new UserFriendlyException("anErrorOccurredPleaseContactSystemAdministrator");
            }

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> AddPasswordUserCommand(AddPasswordUserCommand request)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new UserFriendlyException("mustPasswordSameAsConfirmNewPassword");
            }
            var user = new User();

            user = await _userManager.Users.Where(x => x.Email == request.Email && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("emailUserNotFound");
            }
            //if ((bool)user.IsAddPassword)
            //{
            //    throw new UserFriendlyException("addPasswordBefor");
            //}
            var passwordHasher = new PasswordHasher<User>();
            if (!string.IsNullOrEmpty(request.NewPassword))
                user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword);

            user.IsAddPassword = true;
            //  var result = await _userManager.ChangePasswordAsync(user,"12345678", request.NewPassword);
            var result = await _userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(user);
            }
            else
            {

                throw new UserFriendlyException("anErrorOccurredPleaseContactSystemAdministrator");
            }

            // return Result.Failure(result.Errors.Select(s => s.Description));
            //   return await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        }
        public async Task<UserDto> ForgetPasswordUserCommand(ForgetPasswordUserCommand request)
        {

            var user = new User();

            user = await _userManager.Users.Where(x => x.Email == request.Email && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("emailUserNotFound");
            }

            try
            {
                var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");

                string verificationLink = $"{applicationUrl}/authentication/add-password?email={user.Email}";
                string message = EmailTemplates.ForgetPassword(user.FullName, verificationLink, user.Email);
                _emailHleper.SendEmailAsync(user.Email, message, "اضافة كلمة المرور");
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("probleminSenMail");
            }

            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> ChangePasswordCommand(ChangePasswordCommand request)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                throw new UserFriendlyException("mustPasswordSameAsConfirmNewPassword");
            }
            var user = new User();

            user = await _userManager.Users.Where(x => x.UserName == _auditService.UserName && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("UserNotFound");
            }
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(user);
            }
            else
            {

                throw new UserFriendlyException("anErrorOccurredPleaseContactSystemAdministrator");
            }

            // return Result.Failure(result.Errors.Select(s => s.Description));
            //   return await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        }
        public async Task<UserDto> DeleteUserCommand(DeleteUserCommand request)
        {
            var user = await _contextUser.GetAllIncluding(c => c.UserRoles).FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (user == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            user.IsDeleted = true;
            foreach (var userRole in user.UserRoles)
            {
                userRole.IsDeleted = true;
                await _userRole.SoftDeleteAsync(userRole);
            }
            await _contextUser.SoftDeleteAsync(user);
            await _contextUser.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetById(GetById request)
        {
            var user = await _contextUser.GetAllIncluding(c => c.UserRoles).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var userDto = _mapper.Map<UserDto>(user);

            userDto.Roles = user?.UserRoles?.Select(c => c.RoleId).ToArray();
            return userDto;

        }
        public async Task<string> GetLastCode()
        {
            var lastCode = await _contextUser.GetAll().OrderByDescending(c => c.Id).Where(x => !x.IsDeleted).FirstOrDefaultAsync();
            var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));
            _auditService.Code = code;
            return code;

        }

        public int? GetLastModuleCode()
        {
            var lastCode = _userDetailsModule.GetAll().OrderByDescending(c => c.Code).Where(x => !x.IsDeleted).FirstOrDefault();
            var code = lastCode != null ? lastCode.Code + 1 : 1;
            return code;

        }
        public async Task<int> DeleteListUserCommand(DeleteListUserCommand request)
        {

            var UserList = await _contextUser.GetAllIncluding(c => c.UserRoles).Where(c => request.Ids.Contains(c.Id) && !c.IsDeleted).ToListAsync();

            if (UserList == null)
            {
                throw new UserFriendlyException("Not Found");
            }

            foreach (var user in UserList)
            {
                user.IsDeleted = true;
                await _contextUser.SoftDeleteAsync(user);
                foreach (var userRole in user.UserRoles)
                {
                    userRole.IsDeleted = true;
                    await _userRole.SoftDeleteAsync(userRole);
                }
            }


            var res = await _contextUser.SaveChangesAsync();
            return res;
        }
        public async Task<UserDto> EditUserOwner(UserOwnerDto request)
        {
            var user = new User();

            user = await _userManager.Users.Where(x => x.Id == request.Id && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("emailUserNotFound");
            }

            var existCode = await _userManager.Users.AnyAsync(x => x.Id != user.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");


            user.UserName = request.Id;

            user.FullName = request.FullName;

            user.Email = request.Email;

            user.PhoneNumber = request.PhoneNumber;


            user.IsActive = (bool)request.IsActive;

            user.UpdatedAt = DateTime.UtcNow;

            user.NameAr = request.NameAr;

            user.NameEn = request.NameEn;

            user.Code = request.Code;



            var roles = await _userManager.GetRolesAsync(user);

            var resultRole = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!resultRole.Succeeded)
                throw new UserFriendlyException("errorInRole");


            var rolesDb = await _roleService.GetAllListAsync(x => request.Roles.Contains(x.Id));

            if (rolesDb != null && rolesDb.Count > 0)
                await _userManager.AddToRolesAsync(user, rolesDb.Select(c => c.Name));


            try
            {

                var userPackageList = await _userDetailsPackage.GetAllListAsync(c => c.UserId == user.Id);

                if (userPackageList != null && userPackageList.Count > 0)
                {
                    foreach (var package in userPackageList)
                    {
                        await _userDetailsPackage.DeleteAsync(package);
                    }
                }
                var userModuleList = await _userDetailsModule.GetAllListAsync(c => c.UserId == user.Id);

                if (userModuleList != null && userModuleList.Count > 0)
                {
                    foreach (var module in userModuleList)
                    {
                        await _userDetailsModule.DeleteAsync(module);
                    }
                }
                var userDetailsPackageInsert = _mapper.Map<List<UserDetailsPackage>>(request.UserDetailsPackageDtos);

                foreach (var package in userDetailsPackageInsert)
                {
                    await _userDetailsPackage.InsertAsync(package);
                }
                var userDetailsModuleInsrert = _mapper.Map<List<UserDetailsModule>>(request.UserDetailsModuleDtos);

                foreach (var module in userDetailsModuleInsrert)
                {
                    await _userDetailsModule.InsertAsync(module);
                }
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                throw new UserFriendlyException("error");
            }
            // await _userDetailsModule.SaveChangesAsync();
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateEmail")
            {
                throw new UserFriendlyException("duplicateEmail");
            }
            else if (!result.Succeeded)
            {

                throw new UserFriendlyException("anErrorOccurredPleaseContactSystemAdministrator");
            }

            return _mapper.Map<UserDto>(user);
        }
       
        public async Task<AuthorizedUserDTO> RegisterOwner(TechnicalSupportCommand request)
        {
            if (request is not null)
            {
                var user = new User();
                var lastCode = await _userManager.Users.OrderByDescending(c => c.Code).FirstOrDefaultAsync(x => !x.IsDeleted);
                var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));

                var checkExsit = await _userManager.Users.Where(x => x.Email == request.Email && !x.IsDeleted).FirstOrDefaultAsync();
                if (checkExsit != null)
                {
                    throw new UserFriendlyException("emailNumberFoundBefore");
                }


                var phoneExsit = await _userManager.Users.Where(x => x.PhoneNumber == request.PhoneNumber && !x.IsDeleted).FirstOrDefaultAsync();
                if (phoneExsit != null)
                {
                    throw new UserFriendlyException("NationalNumberFoundBefore");
                }


                user = new User()
                {
                    Id = Guid.NewGuid().ToString(),

                    Email = request.Email,

                    FullName = request.FullName,

                    PhoneNumber = request.PhoneNumber,

                    NormalizedEmail = request.Email,

                    // CreatedBy = _auditService.UserName,
                    CreatedBy = "admin",

                    IsActive = true,

                    CreatedAt = DateTime.UtcNow,

                    UserType = UserType.Owner,

                    NameAr = request.NameAr,

                    NameEn = request.NameEn,

                    IsVerifyCode = false,

                    Code = code,

                    IsAddPassword = false,
                };
                user.UserName = request.UserName;
                user.NormalizedUserName = request.UserName.ToUpper();

                //handle package and Module 
                //if (request.UserType.ToString() == UserType.Owner.ToString())
                //{
                //    var packageUserDetailsExist = await _userDetailsPackage.GetAll().Where(x => x.UserId == user.Id).ToListAsync();
                //    if (packageUserDetailsExist.Count > 0)
                //    {
                //        foreach (var package in packageUserDetailsExist)
                //        {
                //            await _userDetailsPackage.DeleteAsync(package);
                //        }
                //    }
                //}

                var result = await _userManager.CreateAsync(user, request.Password != "" ? request.Password : "123456");
                if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateEmail")
                {
                    // await _userManager.DeleteAsync(user);
                    throw new UserFriendlyException("duplicateEmail");
                }
                else if (!result.Succeeded && result.Errors.FirstOrDefault().Code == "DuplicateUserName")
                {
                    //  await _userManager.DeleteAsync(user);
                    throw new UserFriendlyException("duplicateUserName");
                }
                else if (!result.Succeeded)
                {
                    // await _userManager.DeleteAsync(user);
                    throw new UserFriendlyException("invalidUsername");
                }
                // var roles = request.Roles;
                if (result.Succeeded)
                {
                    try
                    {
                        var token = GenerateJSONWebToken(user);
                        var userSession = new UserToken
                        {
                            UserId = user.Id,
                            Token = token,
                            Expiry = DateTime.Now.AddDays(Convert.ToDouble(_jwtOption.ExpireDays)),
                            IsValid = true
                        };
                        _userToken.Insert(userSession);
                        await _userToken.SaveChangesAsync();
                        var authorizedUserDto = new AuthorizedUserDTO
                        {
                            User = _mapper.Map<TechnicalSupportDto>(user),
                            Token = token
                        };
                        authorizedUserDto.User.IsSubscriped = false;

                        return authorizedUserDto;


                    }
                    catch (Exception ex)
                    {
                        await _userManager.DeleteAsync(user);

                        throw new UserFriendlyException("errorInRegister");
                    }


                }
            }
            throw new UserFriendlyException("registerError");
        }

        public async Task<List<CreateUserDetailsModulesCommand>> CreateUserDetailsModules(List<CreateUserDetailsModulesCommand> command)
        {

            var code = GetLastModuleCode();
            List<UserDetailsModule> resultList = new List<UserDetailsModule>();

            if (command.Count > 0)
            {
                var user = _userManager.Users.Where(x => x.UserName == command[0].username).FirstOrDefaultAsync().Result;


                foreach (var item in command)
                {
                    if (string.IsNullOrEmpty(item.NameAr) || string.IsNullOrWhiteSpace(item.NameAr))
                        throw new UserFriendlyException("Name Ar Required");

                    if (string.IsNullOrEmpty(item.NameEn) || string.IsNullOrWhiteSpace(item.NameEn))
                        throw new UserFriendlyException("Name En Required");

                    if (string.IsNullOrEmpty(user.Id) || string.IsNullOrWhiteSpace(user.Id))
                        throw new UserFriendlyException("User is Required");

                    DateTime? expireyDate = null;

                    if (item.TypeOfSubscription == (int)TypeOfSubscription.MonthlySubscription)
                        expireyDate = item.SubscriptionStartDate.AddMonths(1);

                    if (item.TypeOfSubscription == (int)TypeOfSubscription.YearlyQuarterSubscription)
                        expireyDate = item.SubscriptionStartDate.AddMonths(3);

                    if (item.TypeOfSubscription == (int)TypeOfSubscription.YearlyHalfSubscription)
                        expireyDate = item.SubscriptionStartDate.AddMonths(6);

                    if (item.TypeOfSubscription == (int)TypeOfSubscription.YearlySubscription)
                        expireyDate = item.SubscriptionStartDate.AddYears(1);

                    UserDetailsModule userDetailsModule = new UserDetailsModule
                    {
                        UserId = user.Id,
                        BillPattrenPrice = item.BillPattrenPrice,
                        SubscriptionStartDate = item.SubscriptionStartDate,
                        InstrumentPattrenPrice = item.InstrumentPattrenPrice,
                        IsActive = true,
                        IsFree = item.IsFree,
                        NameAr = item.NameAr,
                        NameEn = item.NameEn,
                        IsDeleted = false,
                        OtherModuleId = item.OtherModuleId,
                        OtherUserFullBuyingSubscriptionPrice = item.OtherUserFullBuyingSubscriptionPrice,
                        OtherUserMonthlySubscriptionPrice = item.OtherUserMonthlySubscriptionPrice,
                        OtherUserYearlySubscriptionPrice = item.OtherUserYearlySubscriptionPrice,
                        SubscriptionExpiaryDate = expireyDate,
                        SubscriptionPrice = item.SubscriptionPrice,
                        TypeOfSubscription = (TypeOfSubscription)item.TypeOfSubscription,
                        BillPattrenNumber = item.BillPattrenNumber,
                        InstrumentPattrenNumber = item.InstrumentPattrenNumber,
                        NumberOfBranches = item.NumberOfBranches,
                        NumberOfCompanies = item.NumberOfCompanies,
                        NumberOfUser = item.NumberOfUser,
                        Code = code


                    };


                    await _userDetailsModuleCtx.InsertAsync(userDetailsModule);
                    resultList.Add(userDetailsModule);
                    //to Add CompanyForEachModule
                    var companies = await _userCompanies.GetAll()
                            .Where(x => x.OrganizationId == command[0].OrganizationId).ToListAsync();
                    if (companies.Count > 0)
                    {
                        foreach (var com in companies)
                        {

                            await _packageModuleCompany.InsertAsync(new PackageModuleCompany
                            {
                                CompanyId = com.Id,
                                ModuleUserDetailsCode = userDetailsModule.Code,
                                PackageUserDetailsId = null
                            });


                        }
                        await _packageModuleCompany.SaveChangesAsync();
                    }

                }


                if (await _userDetailsModuleCtx.SaveChangesAsync() > -1)
                {
                    await _packageModuleCompany.SaveChangesAsync();
                    //for promoCode
                    if (command[0].PromoCode > 0)
                    {
                        await CreateUserPromoCode(command[0].PromoCode, user, null, resultList.Select(x => x.Id).ToList());

                    }
                    //save userPayment 
                    await CreateUserPayment(null, resultList, command[0].PaymentType, command[0].OrganizationId);
                    //for Create database if module is free
                    if (resultList.Count == 1 && resultList[0].IsFree == true)
                    {
                        var check = await CreateDatabase(command[0].OrganizationId, user.Id, user, null, resultList);
                    }
                }

            }

            return command;
        }

        public async Task<CreateUserDetailsPackageCommand> CreateUserPackageDetails(CreateUserDetailsPackageCommand command)
        {
            UserDetailsPackage userDetailsPackage = new UserDetailsPackage();
            DateTime? expireyDate = null;
            if (command != null)
            {
                if (command.TypeOfSubscription == (int)TypeOfSubscription.MonthlySubscription)
                    expireyDate = command.SubscriptionStartDate.AddMonths(1);

                if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlyQuarterSubscription)
                    expireyDate = command.SubscriptionStartDate.AddMonths(3);

                if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlyHalfSubscription)
                    expireyDate = command.SubscriptionStartDate.AddMonths(6);

                if (command.TypeOfSubscription == (int)TypeOfSubscription.YearlySubscription)
                    expireyDate = command.SubscriptionStartDate.AddYears(1);

                var user = _userManager.Users.Where(x => x.UserName == command.username).FirstOrDefaultAsync().Result;


                userDetailsPackage = new UserDetailsPackage
                {
                    UserId = user.Id,
                    InstrumentPattrenNumber = command.InstrumentPattrenNumber,
                    NumberOfBranches = command.NumberOfBranches,
                    NumberOfCompanies = command.NumberOfCompanies,
                    NumberOfUsers = command.NumberOfUsers,
                    SubscriptionStartDate = command.SubscriptionStartDate,
                    BillPattrenNumber = command.BillPattrenNumber,
                    IsActive = true,
                    NameAr = command.NameAr,
                    NameEn = command.NameEn,
                    IsDeleted = false,
                    SubscriptionExpiaryDate = expireyDate,
                    SubscriptionPrice = command.SubscriptionPrice,
                    TypeOfSubscription = (TypeOfSubscription)command.TypeOfSubscription,
                };

                await _userDetailsPackage.InsertAsync(userDetailsPackage);

                //get the modules of the package
                List<UserDetailsModule> userDetailsModule = new List<UserDetailsModule>();
                var packageModule = await _modulesPackagesContext.GetAll().Where(x => x.PackageId == command.PackageId).ToListAsync();
                if (packageModule.Count > 0)
                {
                    var code = GetLastModuleCode();
                    foreach (var item in packageModule)
                    {

                        var modules = await _modulesContext.GetAll().Where(x => x.Id == item.ModuleId).FirstOrDefaultAsync();
                        if (modules != null)
                        {
                            userDetailsModule.Add(new UserDetailsModule
                            {
                                UserId = user.Id,
                                Code = code,
                                BillPattrenPrice = modules.BillPattrenPrice,
                                SubscriptionStartDate = command.SubscriptionStartDate,
                                InstrumentPattrenPrice = modules.InstrumentPattrenPrice,
                                IsActive = true,
                                IsFree = modules.IsFree,
                                NameAr = modules.NameAr,
                                NameEn = modules.NameEn,
                                IsDeleted = false,
                                OtherModuleId = modules.OtherModuleId,
                                OtherUserFullBuyingSubscriptionPrice = modules.OtherUserFullBuyingSubscriptionPrice,
                                OtherUserMonthlySubscriptionPrice = modules.OtherUserMonthlySubscriptionPrice,
                                OtherUserYearlySubscriptionPrice = modules.OtherUserYearlySubscriptionPrice,
                                SubscriptionExpiaryDate = expireyDate,
                                SubscriptionPrice = command.SubscriptionPrice,
                                TypeOfSubscription = (TypeOfSubscription)command.TypeOfSubscription,
                                IsPackageModule = true

                            });
                        }
                    }
                    await _userDetailsModule.InsertRangeAsync(userDetailsModule);
                }



                //handle save transaction of the package or modules
                try
                {
                    if (await _userDetailsPackage.SaveChangesAsync() > -1)
                    {
                        if (await _userDetailsModule.SaveChangesAsync() > -1)
                        {
                            //handle save UserPackageModule data
                            foreach (var item in userDetailsModule)
                            {
                                var data = new UserDetailsPackagesModules
                                {
                                    UserDetailsModuleId = item.Id,
                                    UserDetailsPackageId = userDetailsPackage.Id
                                };
                                await _packageModulesContext.InsertAsync(data);

                                await _packageModulesContext.SaveChangesAsync();
                            }
                        }

                        //for Company Details with Package 
                        var companies = await _userCompanies.GetAll()
                            .Where(x => x.OrganizationId == command.OrganizationId).ToListAsync();
                        if (companies.Count > 0)
                        {
                            foreach (var item in companies)
                            {

                                await _packageModuleCompany.InsertAsync(new PackageModuleCompany
                                {
                                    CompanyId = item.Id,
                                    ModuleUserDetailsCode = null,
                                    PackageUserDetailsId = userDetailsPackage.Id
                                });


                            }
                            await _packageModuleCompany.SaveChangesAsync();
                        }

                        //apply if pormoCode Exist 
                        if (command.PromoCode > 0)
                        {
                            await CreateUserPromoCode(command.PromoCode, user, userDetailsPackage.Id, new List<long>());
                        }
                        //save userPayment 
                        await CreateUserPayment(userDetailsPackage, null, command.PaymentType, command.OrganizationId);

                    }
                }
                catch (Exception ex)
                {
                    await _userDetailsPackage.DeleteAsync(userDetailsPackage);
                    _userDetailsModule.DeleteAll(userDetailsModule);
                    throw new UserFriendlyException(ex.Message);
                }

            }
            return command;
        }

        private string CreateRandomText()
        {
            Random res = new Random();
            String str = "abcdefghijklmnopqrstuvwxyz0123456789";
            int size = 8;
            String randomstring = "";
            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);
                randomstring = randomstring + str[x];
            }
            bool isExsit = _userOrganization.GetAll().Where(x => x.DatabaseName == randomstring).Any();
            if (isExsit == true)
            {
                return CreateRandomText();

            }

            return randomstring;
        }
        public async Task<long> CreateUserOrganizations(OrganizationDto command)
        {
            if (command.OrganizationName == "" && command.OrganizationNameEn == "")
                throw new UserFriendlyException("Organization Name  Required ");

            try
            {
                var user = await _userManager.Users.Where(x => x.UserName == command.Username).FirstOrDefaultAsync();

                string randomstring = CreateRandomText();

                UserOrganization userOrganization = new UserOrganization
                {
                    OrganizationNameEn = command.OrganizationNameEn,
                    OrganizationNameAr = command.OrganizationName,
                    DatabaseName = randomstring,
                    DomainName = _configuration["ServerName"]
                };


                await _userOrganization.InsertAsync(userOrganization);
                await _userOrganization.SaveChangesAsync();

                UserOrgnizationType userType = new UserOrgnizationType
                {
                    UserId = user.Id,
                    UserType = user.UserType.Value,
                    OrganizationId = userOrganization.Id
                };

                await _userOrgnizationType.InsertAsync(userType);
                await _userOrgnizationType.SaveChangesAsync();
                return userOrganization.Id;

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task<List<OrganizationDto>> GetUserOrganizations(string userName)
        {
            var user = await _userManager.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
                return new List<OrganizationDto>();
            var userOrganizationTypeList = await _userOrgnizationType.GetAllListAsync(x => x.UserId == user.Id);
            var organizationIdList = userOrganizationTypeList.Select(x => x.OrganizationId).ToList();
            var userOrganizationList = await _userOrganization.GetAllListAsync(x => organizationIdList.Contains(x.Id));

            var companiesofOrgs = await _userCompanies.GetAll().Where(x => organizationIdList.Contains(x.OrganizationId)).ToListAsync();

            var result = userOrganizationList.Select(org => new OrganizationDto
            {
                Id = org.Id,
                OrganizationNameEn = org.OrganizationNameEn,
                OrganizationName = org.OrganizationNameAr,
                Username = user.UserName,
                IsSubscriped = false,
                IsPaid = false,
            }).ToList();

            foreach (var item in result)
            {
                var companies = companiesofOrgs.Where(x => x.OrganizationId == item.Id).Select(x => x.Id).ToList();

                item.IsSubscriped = _packageModuleCompany.GetAll().Where(x => companies.Contains(x.CompanyId)).Any();
                if (companies.Count > 0)
                {
                    var subscription = await _packageModuleCompany.FirstOrDefaultAsync(x => x.CompanyId == companies[0]);
                    item.IsPaid = await _userPayment.GetAll().Where(x => x.UserModuleCode == subscription.ModuleUserDetailsCode || x.UserPackageId == subscription.PackageUserDetailsId)
                                                        .Select(x => x.IsPaid).FirstOrDefaultAsync();
                }


            }

            return result;
        }

        public async Task<long> CreateUserCompanies(UserCompaniesDto command)
        {
            if (string.IsNullOrEmpty(command.CompanyNameAr) || string.IsNullOrEmpty(command.CompanyNameEn))
                throw new UserFriendlyException("Companies  Required ");

            try
            {
                var OrganizationType = await _userOrgnizationType.GetAll().Where(x => x.OrganizationId == command.OrganizationId).FirstOrDefaultAsync();
                if (OrganizationType is null)
                    throw new UserFriendlyException("Organization not found ");

                OrganizationCompany userCompany = _mapper.Map<OrganizationCompany>(command);

                await _userCompanies.InsertAsync(userCompany);
                await _userCompanies.SaveChangesAsync();


                return userCompany.Id;

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);
            }

        }

        private async Task CreateUserPromoCode(long PromoCodeId, User user, long? userPackage, List<long> userModuleList)
        {
            if (PromoCodeId != default)
            {
                if (userPackage is not null)
                {
                    UserSubscriptionPromoCode userSubscriptionPromoCode = new UserSubscriptionPromoCode
                    {
                        PackageUserDetailsId = userPackage == null ? null : userPackage,
                        PromoCodeId = PromoCodeId,
                        UserId = user.Id
                    };
                    await _userPromoCode.InsertAsync(userSubscriptionPromoCode);
                }

                if (userModuleList.Count > 0)
                {
                    var codeModule = _userDetailsModule.FirstOrDefault(x => x.Id == userModuleList[0]).Code;
                    UserSubscriptionPromoCode userSubscriptionPromoCode = new UserSubscriptionPromoCode
                    {
                        ModuleUserDetailsCode = codeModule,
                        PromoCodeId = PromoCodeId,
                        UserId = user.Id,
                        SubscriptionDate = DateTime.Now,
                    };
                    await _userPromoCode.InsertAsync(userSubscriptionPromoCode);

                }



                try
                {
                    await _userPromoCode.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw new UserFriendlyException(ex.Message);
                }
            }
        }

        private async Task CreateUserPayment(UserDetailsPackage userDetailsPackage, List<UserDetailsModule> userDetailsModule, PaymentType paymentType, long OrganizationId)
        {
            //if subscribed with package 
            UserPayment payment = new UserPayment();
            if (userDetailsPackage is not null)
            {
                payment = new UserPayment
                {
                    IsCancelled = false,
                    IsPaid = false,
                    PaymentType = paymentType,
                    TotalPrice = userDetailsPackage.SubscriptionPrice,
                    UserId = userDetailsPackage.UserId,
                    UserPackageId = userDetailsPackage.Id,
                    UserModuleCode = null
                };
            }
            else if (userDetailsModule.Count > 0)
            {
                var codeModule = _userDetailsModule.FirstOrDefault(x => x.Id == userDetailsModule[0].Id).Code;
                payment = new UserPayment
                {
                    IsCancelled = false,
                    IsPaid = (userDetailsModule[0].IsFree == true && userDetailsModule.Count == 1) == true ? true : false,
                    PaymentType = paymentType,
                    TotalPrice = (userDetailsModule[0].IsFree == true && userDetailsModule.Count == 1) ? 0 : userDetailsModule[0].SubscriptionPrice,
                    UserId = userDetailsModule[0].UserId,
                    UserModuleCode = codeModule,
                    UserPackageId = null
                };
            }

            await _userPayment.InsertAsync(payment);
            if (await _userPayment.SaveChangesAsync() > 0)
            {
                if (paymentType == PaymentType.Online)
                {
                    await _userPaymentOnline.InsertAsync(new UserPaymentOnline
                    {
                        CartAmount = payment.TotalPrice,
                        CartCurrencyCode = "SAR",
                        CartDecription = "Payment Online for Package",
                        CreationDate = DateTime.Now,
                        CartId = new Guid(),
                        PaymentIds = payment.Id.ToString(),
                        OrganizationId = OrganizationId
                    });

                    await _userPaymentOnline.SaveChangesAsync();

                }
            }

        }
        public async Task<List<OrganizationDto>> GetUserCompanies(GetUserCompanyRequest command)
        {
            List<OrganizationDto> companies = new List<OrganizationDto>();
            try
            {
                var user = _userManager.Users.Where(x => x.UserName == command.username).FirstOrDefaultAsync().Result;
                if (user != null)
                {
                    var companyDateIds = _userOrgnizationType.GetAll()
                                         .Where(x => x.UserId == user.Id).Select(x => x.OrganizationId).ToList();

                    if (companyDateIds.Count > 0)
                    {
                        foreach (var companyId in companyDateIds)
                        {
                            var company = _userCompanies.GetAll()
                            .Where(x => x.Id == companyId).FirstAsync();

                            if (company.Result != null && company.Result.IsExtra == command.IsExtra)
                            {
                                var IsSubscriped = _packageModuleCompany.GetAll().Where(x => x.CompanyId == company.Result.Id).ToList();

                                companies.Add(new OrganizationDto
                                {
                                    OrganizationName = company.Result.CompanyNameAr,
                                    OrganizationNameEn = company.Result.CompanyNameEn,
                                    Id = company.Result.Id,
                                    IsSubscriped = (IsSubscriped != null && IsSubscriped.Count > 0) ? true : false
                                });
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException(ex.Message);

            }
            return companies;
        }


        public async Task<long> AddTrialCommand(TrialDto request)
        {
            if (request.moduleId == 0 || request.username == "")
                throw new UserFriendlyException("All Fields are Required");

            try
            {
                var user = await _userManager.Users.Where(x => x.UserName == request.username).FirstOrDefaultAsync();
                var module = await _modulesContext.GetAll().Where(x => x.Id == request.moduleId).FirstOrDefaultAsync();


                if (module != null && user != null)
                {
                    var code = GetLastModuleCode();

                    UserDetailsModule userDetailsModuleDto = new UserDetailsModule
                    {
                        BillPattrenPrice = module.BillPattrenPrice,
                        InstrumentPattrenPrice = module.InstrumentPattrenPrice,
                        IsActive = true,
                        IsFree = module.IsFree,
                        IsPackageModule = false,
                        NameAr = module.NameAr,
                        NameEn = module.NameEn,
                        UserId = user.Id,
                        InstrumentPattrenNumber = module.InstrumentPattrenPrice == null ? 0 : 1,
                        BillPattrenNumber = module.BillPattrenPrice == null ? 0 : 1,
                        NumberOfBranches = 1,
                        NumberOfCompanies = 1,
                        NumberOfUser = 0,
                        OtherUserFullBuyingSubscriptionPrice = module.OtherUserFullBuyingSubscriptionPrice,
                        OtherUserMonthlySubscriptionPrice = module.OtherUserMonthlySubscriptionPrice,
                        OtherUserYearlySubscriptionPrice = module.OtherUserYearlySubscriptionPrice,
                        SubscriptionExpiaryDate = DateTime.Now.AddMonths(1),
                        SubscriptionStartDate = DateTime.Now,
                        SubscriptionPrice = 0,
                        TypeOfSubscription = TypeOfSubscription.MonthlySubscription,
                        Notes = module.Notes,
                        Code = code
                    };

                    await _userDetailsModule.InsertAsync(userDetailsModuleDto);

                    if (await _userDetailsModule.SaveChangesAsync() > -1)
                    {
                        OrganizationCompany userCompany = new OrganizationCompany
                        {
                            CompanyNameAr = request.company,
                            CompanyNameEn = request.company,
                            IsExtra = false,
                            NumberOfBranches = 1,
                            OrganizationId = request.organizationId,
                        };
                        await _userCompanies.InsertAsync(userCompany);
                        if (await _userCompanies.SaveChangesAsync() > -1)
                        {
                            PackageModuleCompany packageModuleCompany = new PackageModuleCompany
                            {
                                CompanyId = userCompany.Id,
                                ModuleUserDetailsCode = userDetailsModuleDto.Id
                            };
                            await _packageModuleCompany.InsertAsync(packageModuleCompany);
                            await _packageModuleCompany.SaveChangesAsync();

                        }
                        UserPayment userPayment = new UserPayment
                        {
                            IsCancelled = false,
                            IsPaid = true,
                            PaymentType = PaymentType.Trial,
                            TotalPrice = 0,
                            UserModuleCode = code,
                            UserId = user.Id
                        };
                        await _userPayment.InsertAsync(userPayment);
                        await _userPayment.SaveChangesAsync();
                        List<UserDetailsModule> userDetails = new List<UserDetailsModule>();
                        userDetails.Add(userDetailsModuleDto);

                        var check = await CreateDatabase(request.organizationId, user.Id, user, null, userDetails);

                        return 1;

                    }
                }

            }
            catch (Exception ex)
            {

                throw new UserFriendlyException(ex.Message);
            }

            return 0;
        }

        public async Task<bool> CreateDatabase(long? orgId, string userId, User user = null, UserDetailsPackage package = null, List<UserDetailsModule> moduleList = null)
        {
            List<long> moduleIds;
            var input = new Common.SharedDto.SettingDataBaseDto();
            if (user == null)
            {
                user = await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();

            }
            if (user != null)
            {
                input.OwnerInfoDto = new UserOwnerInfoDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    NameAr = user.NameAr,
                    NameEn = user.NameEn,
                    Code = user.Code,
                    UserType = user.UserType,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    PassWord = user.PasswordHash,
                    PasswordHash = user.PasswordHash,
                    Roles = null,
                    IsActive = user.IsActive,
                    FullName = user.FullName,
                    NameDatabase = user.FullName
                };
            }



            if (package != null)
            {
                moduleIds = await _packageModulesContext.GetAll()
                               .Where(x => x.UserDetailsPackageId.Equals(package.Id)).Select(x => x.UserDetailsModuleId).ToListAsync();

                if (moduleIds.Count > 0)
                {
                    var modulePackageList = await _userDetailsModule.GetAll().Where(x => x.UserId == user.Id && moduleIds.Contains(x.Id) && x.IsPackageModule == true).ToListAsync();

                    if (modulePackageList != null)
                    {

                        foreach (var module in modulePackageList)
                        {
                            var moduleDataBase = new UserDetailsModuleInfoDto()
                            {
                                Id = module.Id,
                                UserId = module.UserId,
                                NameAr = module.NameAr,
                                NameEn = module.NameEn,
                                BillPattrenPrice = module.BillPattrenPrice,
                                Code = module.Code,
                                InstrumentPattrenPrice = module.InstrumentPattrenPrice,
                                IsFree = module.IsFree,
                                IsPackageModule = module.IsPackageModule,
                                NumberOfUser = module.NumberOfUser,
                                OtherModuleId = module.OtherModuleId,
                                OtherUserFullBuyingSubscriptionPrice = module.OtherUserFullBuyingSubscriptionPrice,
                                OtherUserMonthlySubscriptionPrice = module.OtherUserMonthlySubscriptionPrice,
                                OtherUserYearlySubscriptionPrice = module.OtherUserYearlySubscriptionPrice,

                                NumberOfCompanies = module.NumberOfCompanies,
                                NumberOfBranches = module.NumberOfBranches,
                                BillPattrenNumber = module.BillPattrenNumber,
                                InstrumentPattrenNumber = module.InstrumentPattrenNumber,
                                TypeOfSubscription = module.TypeOfSubscription,
                                SubscriptionPrice = module.SubscriptionPrice,
                                SubscriptionStartDate = module.SubscriptionStartDate,
                                SubscriptionExpiaryDate = module.SubscriptionExpiaryDate,
                                PackgId = (int)package.Id


                            };

                            input.UserDetailsModules.Add(moduleDataBase);
                        }
                    }
                }


            }

            if (package != null)
            {


                var packgDataBase = new UserDetailsPackageInfoDto()
                {
                    Id = package.Id,
                    UserId = package.UserId,
                    NameAr = package.NameAr,
                    NameEn = package.NameEn,
                    NumberOfUsers = package.NumberOfUsers,
                    NumberOfCompanies = package.NumberOfCompanies,
                    NumberOfBranches = package.NumberOfBranches,
                    BillPattrenNumber = package.BillPattrenNumber,
                    InstrumentPattrenNumber = package.InstrumentPattrenNumber,
                    TypeOfSubscription = package.TypeOfSubscription,
                    SubscriptionPrice = package.SubscriptionPrice,
                    SubscriptionStartDate = package.SubscriptionStartDate,
                    SubscriptionExpiaryDate = package.SubscriptionExpiaryDate,



                };

                input.UserDetailsPackage.Add(packgDataBase);


            }
            if (moduleList != null)
            {

                foreach (var module in moduleList)
                {
                    var moduleDataBase = new UserDetailsModuleInfoDto()
                    {
                        Id = module.Id,
                        UserId = module.UserId,
                        NameAr = module.NameAr,
                        NameEn = module.NameEn,
                        BillPattrenPrice = module.BillPattrenPrice,
                        Code = module.Code,
                        InstrumentPattrenPrice = module.InstrumentPattrenPrice,
                        IsFree = module.IsFree,
                        IsPackageModule = module.IsPackageModule,
                        NumberOfUser = module.NumberOfUser,
                        OtherModuleId = module.OtherModuleId,
                        OtherUserFullBuyingSubscriptionPrice = module.OtherUserFullBuyingSubscriptionPrice,
                        OtherUserMonthlySubscriptionPrice = module.OtherUserMonthlySubscriptionPrice,
                        OtherUserYearlySubscriptionPrice = module.OtherUserYearlySubscriptionPrice,

                        NumberOfCompanies = module.NumberOfCompanies,
                        NumberOfBranches = module.NumberOfBranches,
                        BillPattrenNumber = module.BillPattrenNumber,
                        InstrumentPattrenNumber = module.InstrumentPattrenNumber,
                        TypeOfSubscription = module.TypeOfSubscription,
                        SubscriptionPrice = module.SubscriptionPrice,
                        SubscriptionStartDate = module.SubscriptionStartDate,
                        SubscriptionExpiaryDate = module.SubscriptionExpiaryDate,



                    };

                    input.UserDetailsModules.Add(moduleDataBase);
                }
            }
            var userOrganizationTypeListUser = await _userOrgnizationType.GetAllListAsync(x => x.UserId == user.Id);
            var userOrganizationTypeList = await _userOrgnizationType.GetAllListAsync(x => x.OrganizationId == orgId);
            var organizationIdList = userOrganizationTypeList.Select(x => x.OrganizationId).ToList();
            if (organizationIdList != null)
            {
                var userOrganizationList = await _userOrganization.GetAllListAsync(x => organizationIdList.Contains(x.Id));
                if (userOrganizationList != null)
                {
                    var companiesofOrgs = await _userCompanies.GetAll().Where(x => organizationIdList.Contains(x.OrganizationId)).ToListAsync();
                    if (companiesofOrgs != null)
                    {
                        var companies = companiesofOrgs.Select(com => new UserCompaniesInfDto
                        {
                            CompanyId = com.Id,
                            CompanyNameAr = com.CompanyNameAr,
                            CompanyNameEn = com.CompanyNameEn,
                            NumberOfBranches = com.NumberOfBranches,
                            IsExtra = com.IsExtra,
                            OrganizationId = com.OrganizationId
                        }).ToList();
                        input.UserCompaniesDto.AddRange(companies);
                    }
                }


            }



            input.OwnerInfoDto.NameDatabase = _userOrganization.GetAll().Where(x => x.Id == orgId).FirstOrDefault()?.DatabaseName ?? CreateRandomText();
            // input.OwnerInfoDto.NameDatabase =  CreateRandomText();


            var jsonString = await _api.CreateDatabase(input);

            var result = JsonConvert.DeserializeObject<Response>(jsonString);
            if (result.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public async Task<PaymentResponseLinkDto> GetPaymentRequestLinkDto(long orgId)
        {
            PaymentRequestLinkDto payment = new PaymentRequestLinkDto();
            PaymentResponseLinkDto response = new PaymentResponseLinkDto();
            //get any payment Reuqest by org Id
            var userOnlinePayment = await _userPaymentOnline.FirstOrDefaultAsync(x => x.OrganizationId == orgId);
            if (userOnlinePayment != null)
            {
                payment.cart_description = userOnlinePayment.CartDecription;
                payment.cart_amount = userOnlinePayment.CartAmount;
                payment.cart_id = userOnlinePayment.CartId.ToString();
                payment.tran_class = _configuration["Tran_Class"];
                payment.tran_type = _configuration["Tran_Type"];
                payment.cart_currency = userOnlinePayment.CartCurrencyCode;
                payment.profile_id = _configuration["Profile_Id"];

                string payTabsApiEndpoint = "https://secure.paytabs.sa/payment/request";
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var request = new HttpRequestMessage(HttpMethod.Post, payTabsApiEndpoint);

                        var jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(payment);
                        request.Content = new StringContent(jsonRequestData, System.Text.Encoding.UTF8, "application/json");
                        request.Headers.Add("Authorization", _configuration["ServerKey"]);
                        HttpResponseMessage responseData = await client.SendAsync(request);
                        if (responseData.IsSuccessStatusCode)
                        {
                            // Read response content as string
                            string responseContent = await responseData.Content.ReadAsStringAsync();

                            // Handle response data as needed
                            response = JsonConvert.DeserializeObject<PaymentResponseLinkDto>(responseContent);
                            return response;

                        }
                        else
                        {
                            throw new Exception(responseData.StatusCode.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message.ToString());
                    }
                }
            }
            return null;
        }

    }



}
