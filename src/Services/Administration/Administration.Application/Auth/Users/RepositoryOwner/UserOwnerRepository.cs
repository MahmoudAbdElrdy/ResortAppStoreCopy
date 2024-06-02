using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Constants;
using Common.Extensions;
using Common.Interfaces;
using Common.SharedDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nashmi.Services.NPay.Data.ExternalServices.PaymentApi;
using Newtonsoft.Json;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Auth.Users.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.Modules.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto;
using ResortAppStore.Services.Administration.Domain;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using User = AuthDomain.Entities.Auth.User;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.UserLogin.Repository
{
    public class UserOwnerRepository : IUserOwnerRepository
    {

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

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

        public UserOwnerRepository(
        UserManager<User> userManager,
        IGRepository<UserOrganization> userOrganization,
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
        IMapper mapper, IGRepository<UserDetailsPackagesModules> packageModulesContext, ISettingErpApi api)
        {

            _mapper = mapper;
            _userManager = userManager;

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
        }


        public async Task<UserOwnerDto> GetUserOwner(GetById request)
        {
            var user = await _contextUser.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);

            var userDto = _mapper.Map<UserOwnerDto>(user);

            var package = await _userDetailsPackage.GetAll().Where(x => x.UserId == request.Id).ToListAsync();

            var module = await _userDetailsModule.GetAll().Where(x => x.UserId == request.Id && x.IsPackageModule == false).ToListAsync();
            var i = 0;
            var mergedUserDetails = new List<MergedUserDetailsDto>();
            if (package != null)
            {
                userDto.UserDetailsPackageDtos = _mapper.Map<List<UserDetailsPackageDto>>(package);

                foreach (var item in userDto.UserDetailsPackageDtos)
                {
                    var itemMerge = new MergedUserDetailsDto();
                    itemMerge.Id = i++;
                    itemMerge.UserId = item.UserId;
                    itemMerge.UserDetailsPackageId = item.Id;
                    itemMerge.NameAr = item.NameAr;
                    itemMerge.NameEn = item.NameEn;
                    itemMerge.SubscriptionExpiaryDate = item.SubscriptionExpiaryDate;
                    itemMerge.SubscriptionStartDate = item.SubscriptionStartDate;
                    itemMerge.TypeOfSubscription = item.TypeOfSubscription;
                    itemMerge.Code = item.Code;
                    mergedUserDetails.Add(itemMerge);


                }

            }
            if (module != null)
            {
                userDto.UserDetailsModuleDtos = _mapper.Map<List<UserDetailsModuleDto>>(module);

                foreach (var item in userDto.UserDetailsModuleDtos)
                {
                    var itemMerge = new MergedUserDetailsDto();

                    itemMerge.UserId = item.UserId;
                    itemMerge.Id = i++;
                    itemMerge.UserDetailsModuleId = item.Id;
                    itemMerge.NameAr = "برامج مخصصة";
                    itemMerge.NameEn = "Custom Module";
                    itemMerge.SubscriptionExpiaryDate = item.SubscriptionExpiaryDate;
                    itemMerge.SubscriptionStartDate = item.SubscriptionStartDate;
                    itemMerge.TypeOfSubscription = item.TypeOfSubscription;
                    itemMerge.Code = item.Code;
                    if (!mergedUserDetails.Any(c => c.Code == item.Code))
                    {
                        mergedUserDetails.Add(itemMerge);

                    }



                }

            }


            userDto.MergedUserDetailsDto = mergedUserDetails;
            return userDto;

        }

        public async Task<UserDetailsPackageDto> GetUserDetailsPackage(GetUserDetailsPackageById request)
        {
            var userDetailsPackageDtos = new UserDetailsPackageDto();

            var package = await _userDetailsPackage.GetAll().Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (package != null)
            {
                userDetailsPackageDtos = _mapper.Map<UserDetailsPackageDto>(package);


                userDetailsPackageDtos.ModuleIds = await _packageModulesContext.GetAll()
                                       .Where(x => x.UserDetailsPackageId.Equals(userDetailsPackageDtos.Id)).Select(x => x.UserDetailsModuleId).ToListAsync();
                if (userDetailsPackageDtos.ModuleIds.Any())
                {
                    var moduleList = await _userDetailsModule.GetAll().Where(x => userDetailsPackageDtos.ModuleIds.Contains(x.Id) && x.IsPackageModule == true).ToListAsync();

                    userDetailsPackageDtos.UserDetailsModuleDtos = _mapper.Map<List<UserDetailsModuleDto>>(moduleList);


                }

                var paymentPackageEntity = _userPayment.FirstOrDefault(c => c.UserPackageId == request.Id);
                if (paymentPackageEntity != null)
                {
                    userDetailsPackageDtos.IsPaid = paymentPackageEntity?.IsPaid;
                    userDetailsPackageDtos.PaymentType = paymentPackageEntity?.PaymentType;
                    userDetailsPackageDtos.PaymentId = paymentPackageEntity?.Id;

                }



            }

            return userDetailsPackageDtos;

        }
        public async Task<List<UserDetailsModuleDto>> GetUserDetailsModule(GetUserDetailsModuleByCode request)
        {
            var userDetailsModuleDto = new List<UserDetailsModuleDto>();

            var module = await _userDetailsModule.GetAll().Where(x => x.Code == request.Code && x.IsPackageModule == false).ToListAsync();

            if (module != null)
            {
                userDetailsModuleDto = _mapper.Map<List<UserDetailsModuleDto>>(module);

                foreach (var payment in userDetailsModuleDto)
                {
                    var paymentEntity = _userPayment.FirstOrDefault(c => c.UserModuleCode == request.Code);
                    payment.IsPaid = paymentEntity?.IsPaid;
                    payment.PaymentType = paymentEntity?.PaymentType;
                    payment.PaymentId = paymentEntity?.Id;
                    payment.UserDetailsModuleId = payment.Id;
                }

            }

            return userDetailsModuleDto;

        }

        public async Task<bool> EditUserPayment(EditUserPaymentDto request)
        {

            var payment = await _userPayment.FirstOrDefaultAsync(c => c.Id == request.PaymentId);

            if (payment == null)
            {
                throw new UserFriendlyException("Payment Not Found");
            }

            payment.IsPaid = (bool)request.IsPaid;

            await _userPayment.UpdateAsync(payment);
            // await _userDetailsPackage.UpdateAsync(customer);
            if (payment.IsPaid == true)
            {
                //for custom Modules cash Payment
                if (request.Code != null)
                {


                    if (request.Code != default && request.Code != null)
                    {

                        var moduleList = await _userDetailsModule.GetAll().Where(x => x.Code == payment.UserModuleCode).ToListAsync();

                        var company = await _packageModuleCompany.GetAll().Where(x => x.ModuleUserDetailsCode == request.Code).FirstOrDefaultAsync();

                        var comapnyId = company?.CompanyId;

                        var userCompany = await (_userCompanies.GetAll().Where(c => c.Id == comapnyId).FirstOrDefaultAsync());
                       
                        if (userCompany != null)
                        {
                            var orgId = userCompany?.OrganizationId;
                        
                            var package = _userDetailsPackage.GetAll().AsNoTracking().Where(x => x.Id == payment.UserPackageId).FirstOrDefault();

                            if (orgId != default)
                            {
                                var check = await CreateDatabase(orgId, request.UserId, null, null, moduleList);
                            }
                        }


                        //    var check = await CreateDatabase(null, request.UserId, null, null, moduleList);
                    }
                }
                //for package cash payment
                else
                {

                    var comapnyId = _packageModuleCompany.GetAll().Where(x => x.PackageUserDetailsId == payment.UserPackageId).FirstOrDefault().CompanyId;

                    var userCompany = await (_userCompanies.GetAll().Where(c => c.Id == comapnyId).FirstOrDefaultAsync());
                    if (userCompany != null)
                    {
                        var orgId = userCompany?.OrganizationId;
                        var package = _userDetailsPackage.GetAll().AsNoTracking().Where(x => x.Id == payment.UserPackageId).FirstOrDefault();

                        if (orgId != default)
                        {
                            var check = await CreateDatabase(orgId, request.UserId, null, package, null);
                        }
                    }

                }

            }

            await _userDetailsPackage.SaveChangesAsync();
            return true;
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



    }
}
