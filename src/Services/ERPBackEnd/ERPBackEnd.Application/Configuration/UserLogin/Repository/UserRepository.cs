using AutoMapper;
using Common.Enums;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Options;
using Common.Services.Service;
using Configuration.Entities;
using CRM.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Auth.Users.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AuthorizedUserDTO = ResortAppStore.Services.ERPBackEnd.Application.Auth.UserLogin.Dto.AuthorizedUserDTO;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IGRepository<Company> _context;
        private readonly IGRepository<UsersCompany> _contextUserCompany;

        private readonly IGRepository<UserCompaniesBranch> _contextUserCompaniesBranch;
        private readonly IMapper _mapper;
        private readonly IGRepository<Branch> _contextBranch;
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
        private readonly IGRepository<Branch> _branchService;
        private readonly DeleteService _deleteService;
        IGRepository<BranchPermission> _gBranchPermission { get; set; }
        public UserRepository(IGRepository<Company> context,
            IGRepository<Branch> contextBranch,
            UserManager<User> userManager,
              IConfiguration configuration,
               IGRepository<UsersCompany> contextUserCompany,
        JwtOption jwtOption,
            IEmailHelper emailHleper,
            IStringLocalizer<UserLoginCommand> stringLocalizer,
            IGRepository<User> contextUser,
                       IAuditService auditService,
        IGRepository<Role> roleService,
        IGRepository<UserRole> userRole,
        IGRepository<Branch> branchService,
        DeleteService deleteService,
        IGRepository<UserCompaniesBranch> contextUserCompaniesBranch, IGRepository<BranchPermission> gBranchPermission,
        IMapper mapper)
        {
            _contextUserCompaniesBranch = contextUserCompaniesBranch;
            _context = context;
            _mapper = mapper;
            _contextBranch = contextBranch;
            _userManager = userManager;
            _configuration = configuration;
            _jwtOption = jwtOption;
            _emailHleper = emailHleper;
            _stringLocalizer = stringLocalizer;
            _contextUser = contextUser;
            _auditService = auditService;
            _roleService = roleService;
            _branchService = branchService;
            _userRole = userRole;
            _deleteService = deleteService;
            _contextUserCompany = contextUserCompany;
            _gBranchPermission = gBranchPermission;
        }
        public async Task<List<CompanyDto>> GetAllCompanyQuery()
        {
            var companyList = await _context.GetAllListAsync(x => !x.IsDeleted);

            return _mapper.Map<List<CompanyDto>>(companyList);

        }
        public async Task<List<BranchDto>> GetBranchesByUserId(string userId,long companyId)
        {
            
                var userCompany= _contextUserCompany.GetAll().Where(x => !x.IsDeleted && x.UserId == userId).FirstOrDefaultAsync().GetAwaiter().GetResult();
                var branchesList = _contextBranch.GetAllIncluding(c => c.Company).Where(c => !c.IsDeleted && c.CompanyId == companyId).ToList(); ;

                return _mapper.Map<List<BranchDto>>(branchesList);
            
         
;

        }

        public async Task<List<CompanyDto>> GetAllCompanyByOrganization(long organizationId) 
        {
           
            var companyList = await _context.GetAllListAsync(x => !x.IsDeleted&&x.OrganizationId== organizationId);
        
            return _mapper.Map<List<CompanyDto>>(companyList);

        }
        public async Task<List<BranchDto>> GetAllBranches(GetAllBranchesQuery request)
        {
            if (request.companies == null)
                return new List<BranchDto>();
            var branchList = _contextBranch.GetAllIncluding(c => c.Company).Where(c => !c.IsDeleted && request.companies.Contains(c.CompanyId.Value)).ToList();


            return _mapper.Map<List<BranchDto>>(branchList);

        }
        public async Task<AuthorizedUserDTO> LoginCommand(LoginCommand request)
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
            //var user = await _context.GetAllIncluding(c => c.UserRoles).Include(c => c.UsersCompanies).ThenInclude(c => c.UserCompaniesBranchs).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var personalUserCompany = await _userManager
                .Users.Where(x => x.Id == personalUser.Id)
                .Include(c => c.UsersCompanies).
                 ThenInclude(c => c.Company).
                //  ThenInclude(c => c.Branches).
                // ThenInclude(c=>c.UsersCompany.Company).
                // ThenInclude(c=>c.Branches).
                FirstOrDefaultAsync();
            var companies = personalUserCompany?.UsersCompanies?.Select(c => c.Company).ToList();



            // var branches = companies.SelectMany(c=>c.Branches).ToList();


            var authorizedUserDto = new AuthorizedUserDTO
            {
                User = _mapper.Map<UserLoginDto>(personalUser),
                //   Token = GenerateJSONWebToken(personalUser, request),
                Companies = _mapper.Map<List<CompanyDto>>(companies),
                //  Branches=_mapper.Map<List<BranchDto>>(branches)
            };


            return authorizedUserDto;
        }
        public async Task<AuthorizedUserDTO> LoginCompanyCommand(LoginCompanyCommand request)
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
                throw new UserFriendlyException("usernameOrPassWordNotCorrect");

            }
            //var user = await _context.GetAllIncluding(c => c.UserRoles).Include(c => c.UsersCompanies).ThenInclude(c => c.UserCompaniesBranchs).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var personalUserCompany = await _userManager
                .Users.Where(x => x.Id == personalUser.Id)
                .Include(c => c.UsersCompanies).
                ThenInclude(c => c.UserCompaniesBranchs).
                FirstOrDefaultAsync();
            var companies = personalUserCompany?.UsersCompanies?.Select(c => (long)c.CompanyId.Value).ToArray().Contains(request.CompanyId);

            if ((bool)!companies)
            {
                throw new UserFriendlyException("companyNotCorrect");
            }

            var branches = personalUserCompany?.UsersCompanies?.SelectMany(c => c.UserCompaniesBranchs).Select(c => (long)c.BranchId).Contains(request.BranchId);

            if ((bool)!branches)
            {
                throw new UserFriendlyException("branchNotCorrect");
            }
            var authorizedUserDto = new AuthorizedUserDTO
            {
                User = _mapper.Map<UserLoginDto>(personalUser),
                Token = GenerateJSONWebToken(personalUser, request),
            };
            if (personalUser.LoginCount == null || personalUser.LoginCount == 0)
            {
                try
                {
                    personalUser.LoginCount = 0;
                    var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");

                    string verificationLink = $"{applicationUrl}/authentication/add-password?email={personalUser.Email}";
                    string message = EmailTemplates.AddPassword(personalUser.FullName, verificationLink);
                    _emailHleper.SendEmailAsync(personalUser.Email, message, "اضافة كلمة المرور");


                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("probleminSenMail");
                }
            }

            personalUser.LoginCount++;
            await _userManager.UpdateAsync(personalUser);

            return authorizedUserDto;
        }
        private string GenerateJSONWebToken(User user, LoginCompanyCommand command)
        {

            var audience = _configuration["JwtOption:Audience"];

            var issuer = _configuration["JwtOption:Issuer"];

            var claims = (new List<Claim>() {
                    new Claim("userLoginId", user.Id.ToString()),
                    new Claim("companyId", command.CompanyId.ToString()),
                    new Claim("branchId",command.BranchId.ToString()),
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
        public async Task<UserLoginDto> Register(UserLoginCommand request)
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


            return _mapper.Map<UserLoginDto>(user);
        }
        public async Task<List<UserDto>> GetAllUserQuery()
        {
            var userList = await _contextUser.GetAllListAsync(x => !x.IsDeleted);

            return _mapper.Map<List<UserDto>>(userList);

        }
        public async Task<PaginatedList<UserDto>> GetAllUsersWithPaginationCommand(GetAllUsersWithPaginationCommand request)
        {
            var query = _contextUser.GetAllIncluding(x => x.UserRoles).Where(c => !c.IsDeleted && c.UserType == Common.Enums.UserType.Technical);

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
                var lastCode = await _userManager.Users.OrderByDescending(c => c.Code).FirstOrDefaultAsync(x => !x.IsDeleted);

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
            var checkExsitFullName = await _userManager.Users.Where(x => x.FullName == request.FullName && !x.IsDeleted).FirstOrDefaultAsync();

            if (checkExsitFullName != null)
            {
                throw new UserFriendlyException("userNameFoundBefore");
            }

            var phoneExsit = await _userManager.Users.Where(x => x.PhoneNumber == request.PhoneNumber && !x.IsDeleted).FirstOrDefaultAsync();

            if (phoneExsit != null)
            {
                throw new UserFriendlyException("phoneNumberFoundBefore");
            }
            var branchesList = _branchService.GetAllList().ToList();

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

                LoginCount = 1,

                IsAddPassword = false,

            };
            user.UsersCompanies = new List<UsersCompany>();
            request.CompaniesUserDtos = request.CompaniesUserDtos.DistinctBy(c => c.CompanyId).ToList();
            if (request.CompaniesUserDtos != null && request.CompaniesUserDtos.Count() > 0)
            {
                user.UsersCompanies = new List<UsersCompany>();
                foreach (var company in request.CompaniesUserDtos)
                {
                    var userCompany = new UsersCompany();
                    userCompany.CompanyId = company.CompanyId;
                    userCompany.UserCompaniesBranchs = new List<UserCompaniesBranch>();
                    foreach (var branch in company.Branches)
                    {
                        var userCompanyBranch = new UserCompaniesBranch();
                        userCompanyBranch.BranchId = branch;
                        userCompany.UserCompaniesBranchs.Add(userCompanyBranch);
                        // user.UsersCompanies.Add(userCompanyBranch);
                    }
                    user.UsersCompanies.Add(userCompany);
                }

            }

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
                if (request.branchPermissions != null && request.branchPermissions.Count > 0)
                {
                    await CreateBranchPermission(request.branchPermissions, user.Id);
                }
              
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

            // user = await _userManager.Users.Include(c => c.UserRoles).ThenInclude(c => c.Role).Include(x => x.UsersCompanies).ThenInclude(c=>c.UserCompaniesBranchs).Where(x => x.Id == request.Id && !x.IsDeleted).FirstOrDefaultAsync();
            user = await _userManager.Users.Include(x => x.UsersCompanies).ThenInclude(c => c.UserCompaniesBranchs).Where(x => x.Id == request.Id && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UserFriendlyException("emailUserNotFound");
            }

            var existCode = await _userManager.Users.AnyAsync(x => x.Id != user.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

            var checkExsitFullName = await _userManager.Users.Where(x => x.Id != user.Id && x.FullName == request.FullName && !x.IsDeleted).FirstOrDefaultAsync();

            if (checkExsitFullName != null)
            {
                throw new UserFriendlyException("userNameFoundBefore");
            }
            user.UserName = request.Id;

            user.FullName = request.FullName;

            user.Email = request.Email;

            user.PhoneNumber = request.PhoneNumber;


            user.IsActive = (bool)request.IsActive;

            user.UpdatedAt = DateTime.UtcNow;

            user.NameAr = request.NameAr;

            user.NameEn = request.NameEn;

            user.Code = request.Code;

            foreach (var company in user.UsersCompanies)
            {
                foreach (var branch in company.UserCompaniesBranchs)
                {
                    //  await  _userCompaniesBranchService.DeleteAsync(branch);
                }
                //await _usersCompanyService.DeleteAsync(company);
            }
            user.UsersCompanies = new List<UsersCompany>();
            request.CompaniesUserDtos = request.CompaniesUserDtos.DistinctBy(c => c.CompanyId).ToList();
            if (request.CompaniesUserDtos != null && request.CompaniesUserDtos.Count() > 0)
            {
                user.UsersCompanies = new List<UsersCompany>();
                foreach (var company in request.CompaniesUserDtos)
                {
                    var userCompany = new UsersCompany();
                    userCompany.CompanyId = company.CompanyId;
                    userCompany.UserCompaniesBranchs = new List<UserCompaniesBranch>();
                    foreach (var branch in company.Branches)
                    {
                        var userCompanyBranch = new UserCompaniesBranch();
                        userCompanyBranch.BranchId = branch;
                        userCompany.UserCompaniesBranchs.Add(userCompanyBranch);
                        // user.UsersCompanies.Add(userCompanyBranch);
                    }
                    user.UsersCompanies.Add(userCompany);
                }

            }


            var roles = await _userManager.GetRolesAsync(user);

            var resultRole = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!resultRole.Succeeded)
                throw new UserFriendlyException("errorInRole");


            var rolesDb = await _roleService.GetAllListAsync(x => request.Roles.Contains(x.Id));

            if (rolesDb != null && rolesDb.Count > 0)
                await _userManager.AddToRolesAsync(user, rolesDb.Select(c => c.Name));

            var result = await _userManager.UpdateAsync(user);
            if (request.branchPermissions != null && request.branchPermissions.Count > 0)
            {
                await EditBranchPermission(request.branchPermissions);
            }
          
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
                throw new UserFriendlyException("password-no-match");
            }
            if (request.NewPassword == request.OldPassword)
            {
                throw new UserFriendlyException("newPasswordMEdit");

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

                throw new UserFriendlyException("passWordNotCorrect");
            }

          
        }
        public async Task<UserDto> DeleteUserCommand(DeleteUserCommand request)
        {
            var user = await _contextUser.GetAllIncluding(c => c.UserRoles).FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (user == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            user.IsDeleted = true;
            var excluded = new List<string>() { "AspNetRoleClaims", "AspNetUserClaims", "UsersCompanies", "AspNetUserRoles", "AspNetUserTokens", "AspNetUserLogins" };
            var isDeleted = await _deleteService.ScriptCheckDeleteExcluded("AspNetUsers", "Id", user.Id, excluded);

            if (!isDeleted)
                throw new UserFriendlyException("can't-delete-record");
            foreach (var userRole in user.UserRoles)
            {
                userRole.IsDeleted = true;
                await _userRole.SoftDeleteAsync(userRole);
            }


            await _contextUser.SoftDeleteAsync(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetById(GetById request)
        {
            var user = await _contextUser.GetAllIncluding(c => c.UserRoles).Include(c => c.UsersCompanies).ThenInclude(c => c.UserCompaniesBranchs).FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);
            var userDto = _mapper.Map<UserDto>(user);

            userDto.Roles = user?.UserRoles?.Select(c => c.RoleId).ToArray();
            userDto.Companies = user?.UsersCompanies?.Select(c => (long)c.CompanyId).ToArray();
            var branches = user?.UsersCompanies?.SelectMany(c => c.UserCompaniesBranchs).ToList();
            userDto.Branches = branches?.Select(c => (long)c.BranchId).ToArray();
            return userDto;

        }
        public async Task<string> GetLastCode()
        {
            var lastCode = await _contextUser.GetAll().OrderByDescending(c => c.Code).Where(x => !x.IsDeleted).FirstOrDefaultAsync();
            var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));
            _auditService.Code = code;
            return code;

        }
        public async Task<int> DeleteListUserCommand(DeleteListUserCommand request)
        {

            var userList = await _contextUser.GetAllIncluding(c => c.UserRoles).Where(c => request.Ids.Contains(c.Id) && !c.IsDeleted).ToListAsync();

            if (userList == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            foreach (var id in request.Ids)
            {
                var excluded = new List<string>() { "AspNetRoleClaims", "AspNetUserClaims", "UsersCompanies", "AspNetUserRoles", "AspNetUserTokens", "AspNetUserLogins" };
                var isDeleted = await _deleteService.ScriptCheckDeleteExcluded("AspNetUsers", "Id", id, excluded);


                if (!isDeleted)
                    throw new UserFriendlyException("can't-delete-some-records");

            }
            foreach (var user in userList)
            {
                user.IsDeleted = true;
                await _contextUser.SoftDeleteAsync(user);
                foreach (var userRole in user.UserRoles)
                {
                    userRole.IsDeleted = true;
                    await _userRole.SoftDeleteAsync(userRole);
                }
            }


            var res = await _context.SaveChangesAsync();
            return res;
        }

        public  string GetRoleByUserId(string userId)
        {
            var role = _userRole.GetAll().FirstOrDefaultAsync(x => x.UserId == userId).GetAwaiter().GetResult();
            return role.RoleId;

        }
        public async Task<bool> EditBranchPermission(List<BranchPermissionDto> branchPermissions)
        {

            foreach (var permission in branchPermissions)
            {
                var permissionDb = await _gBranchPermission.FirstOrDefaultAsync(permission.Id);
                permissionDb.IsChecked = permission.IsChecked;
                await _gBranchPermission.UpdateAsync(permissionDb);
            }
            await _gBranchPermission.SaveChangesAsync();

            return true;
        }
        public async Task<bool> CreateBranchPermission(List<BranchPermissionDto> branchPermissions,string userId) 
        {
            foreach (var input in branchPermissions)
            {
                //if (!branchPermissions.Any(c => c.BranchId == Convert.ToInt64(input.BranchId)))
                //{
                //    var entities = new[]
                //   {
                //  new BranchPermission()
                //  {
                //   IsActive = true,
                //    IsChecked = input.IsChecked,
                //    IsDeleted = false,
                //    CreatedAt = DateTime.Now,
                //   BranchId =Convert.ToInt64(input.BranchId),
                //    ActionName = "Show",
                //    ActionNameEn = "Show",
                //    ActionNameAr = "عرض",
                //    UserId=userId

                //},
                //   new BranchPermission()
                //  {
                //   IsActive = true,
                //   IsChecked = input.IsChecked,
                //    IsDeleted = false,
                //    CreatedAt = DateTime.Now,
                //  BranchId =Convert.ToInt64(input.BranchId),
                //    ActionName = "Add",
                //    ActionNameEn = "Add",
                //    ActionNameAr = "اضافة",
                //     UserId=userId
                //},
                //    new BranchPermission()
                //  {
                //   IsActive = true,
                //  IsChecked = input.IsChecked,
                //    IsDeleted = false,
                //    CreatedAt = DateTime.Now,
                //  BranchId =Convert.ToInt64(input.BranchId),
                //    ActionName = "Edit",
                //    ActionNameEn = "Edit",
                //    ActionNameAr = "تعديل",
                //     UserId=userId
                //},
                //     new BranchPermission()
                //  {
                //   IsActive = true,
                //    IsChecked = input.IsChecked,
                //    IsDeleted = false,
                //    CreatedAt = DateTime.Now,
                //     BranchId =Convert.ToInt64(input.BranchId),
                //    ActionName = "Delete",
                //    ActionNameEn = "Delete",
                //    ActionNameAr = "حذف",
                //     UserId=userId
                //}
                //};
                //    await _gBranchPermission.InsertRangeAsync(entities);
                //}
               
                await _gBranchPermission.InsertAsync(new BranchPermission()
                {
                    IsActive = true,
                    IsChecked = input.IsChecked,
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    BranchId = Convert.ToInt64(input.BranchId),
                    ActionName = input.ActionName,
                    ActionNameEn = input.ActionNameEn,
                    ActionNameAr = input.ActionNameAr,
                    UserId = userId

                });


            }

            await _gBranchPermission.SaveChangesAsync();
            //foreach (var permission in branchPermissions)
            //{
            //    var permissionDb = await _gBranchPermission.FirstOrDefaultAsync(permission.Id);
            //    permissionDb.IsChecked = permission.IsChecked;
            //    await _gBranchPermission.UpdateAsync(permissionDb);
            //}
            //await _gBranchPermission.SaveChangesAsync();

            return true;
        }
    }
}
