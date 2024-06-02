using AutoMapper;
using Common.Extensions;
using Common.Helper;
using Common.Infrastructures;
using Common.Interfaces;
using CRM.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ResortAppStore.Repositories;
using ResortAppStore.Services.Administration.Application.Features.Customers.Dto;
using ResortAppStore.Services.Administration.Application.Services;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Extensions;
using Azure;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace ResortAppStore.Services.Administration.Application.Features.Customers.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper _mapper;
        private readonly IGRepository<Customer> _context;
        private readonly IAuditService _auditService;
        private readonly IEmailHelper _emailHleper;
        private readonly IConfiguration _configuration;
        private readonly IGRepository<CustomerSubscription> _contextCustomerSubscription;
        private readonly IServiceProvider _serviceProvider;

        public CustomerRepository(
            IGRepository<Customer> userManager, IMapper mapper,
            IConfiguration configuration, IAuditService auditService,
            IServiceProvider serviceProvider,
            IEmailHelper emailHleper, IGRepository<CustomerSubscription> contextCustomerSubscription)
        {
            _context = userManager;
            _mapper = mapper;
            _auditService = auditService;
            _emailHleper = emailHleper;
            _configuration = configuration;
            _contextCustomerSubscription = contextCustomerSubscription;
            _serviceProvider = serviceProvider;

        }
        public async Task<CustomerDto> CreateCustomerCommand(CreateCustomerCommand request)
        {
            var customer = new Customer();
            if (request.Code == _auditService.Code)
            {
                var lastCode = await _context.GetAll().OrderByDescending(c => c.Id).FirstOrDefaultAsync(x => !x.IsDeleted);

                var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));

                if (request.Code != code)
                {
                    var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);

                    request.Code = code;
                }
            }
            else
            {
                var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Code == request.Code && !x.IsDeleted);
                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }


            var checkExsit = await _context.FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsDeleted);

            if (checkExsit != null)
            {
                throw new UserFriendlyException("emailNumberFoundBefore");
            }

            var phoneExsit = await _context.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber && !x.IsDeleted);

            if (phoneExsit != null)
            {
                throw new UserFriendlyException("PhoneFoundBefore");
            }


            customer = new Customer()
            {

                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                IsActive = (bool)request.IsActive,
                CreatedAt = DateTime.UtcNow,
                NameAr = request.NameAr,
                NameEn = request.NameEn,
                IsVerifyCode = false,
                Code = request.Code,
                BusinessId = request.BusinessId,
                CountryId = request.CountryId,
                CompanySize = request.CompanySize,
                MultiBranches = request.MultiBranches,
                MultiCompanies = request.MultiCompanies,
                NumberOfBranch = request.NumberOfBranch,
                NumberOfCompany = request.NumberOfCompany,
                CreatedBy = _auditService.UserId,
                IsDeleted = false,
                PassWord = "12345678",
                VerifyCode = GenerateRandom.RandomNumber(10),
                ServerName = _configuration.GetValue<string>("ServerName"),
                DatabaseName = "ResortERP_" + request.Code,
                SubDomain = _configuration.GetValue<string>("SubDomain")

            };
            //CreateDatabase("ResortERP_" + request.Code);
            await CreateSubdomain(request.NameEn);


            try
            {
                var applicationUrl = _configuration.GetValue<string>("ApplicationUrl");

                string verificationLink = $"{applicationUrl}/authentication/verification-code?email={customer.Email}";

                string message = EmailTemplates.VerifyCode(customer.NameAr, verificationLink, customer.VerifyCode);

                _emailHleper.SendEmailAsync(customer.Email, message, "Verification Code");

            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("probleminSendMail");
            }

            await _context.InsertAsync(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);


        }
        public async Task<CustomerSubscriptionDto> CreateCustomerSubscriptionCommand(CreateCustomerSubscriptionCommand request)
        {
            var customerSubscription = _mapper.Map<CustomerSubscription>(request.InputDto);


            await _contextCustomerSubscription.InsertAsync(customerSubscription);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerSubscriptionDto>(customerSubscription);


        }
        public async Task<CustomerDto> DeleteCustomerCommand(DeleteCustomerCommand request)
        {
            var customer = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (customer == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            customer.IsDeleted = true;

            await _context.SoftDeleteAsync(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<CustomerSubscriptionDto> DeleteCustomerSubscriptionCommand(DeleteCustomerSubscriptionCommand request)
        {
            var customerSubscription = await _contextCustomerSubscription.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (customerSubscription == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            customerSubscription.IsDeleted = true;

            await _contextCustomerSubscription.SoftDeleteAsync(customerSubscription);
            await _contextCustomerSubscription.SaveChangesAsync();
            return _mapper.Map<CustomerSubscriptionDto>(customerSubscription);
        }
        public async Task<int> DeleteListCustomerCommand(DeleteListCustomerCommand request)
        {

            var CustomerList = await _context.GetAllListAsync(c => request.Ids.Contains(c.Id));

            if (CustomerList == null)
            {
                throw new UserFriendlyException("Not Found");
            }

            foreach (var Customer in CustomerList)
            {
                Customer.IsDeleted = true;
                await _context.SoftDeleteAsync(Customer);
            }


            var res = await _context.SaveChangesAsync();
            return res;
        }
        public async Task<int> DeleteListCustomerSubscriptionCommand(DeleteListCustomerSubscriptionCommand request)
        {

            var customerSubscriptionList = await _contextCustomerSubscription.GetAllListAsync(c => request.Ids.Contains(c.Id));

            if (customerSubscriptionList == null)
            {
                throw new UserFriendlyException("Not Found");
            }

            foreach (var CustomerSubscription in customerSubscriptionList)
            {
                CustomerSubscription.IsDeleted = true;
                await _contextCustomerSubscription.SoftDeleteAsync(CustomerSubscription);
            }


            var res = await _contextCustomerSubscription.SaveChangesAsync();
            return res;
        }
        public async Task<CustomerDto> EditCustomerCommand(EditCustomerCommand request)
        {
            var customer = await _context.FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (customer == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var existCode = await _context.GetAllAsNoTracking().AnyAsync(x => x.Id != customer.Id && x.Code == request.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.IsActive = (bool)request.IsActive;
            customer.UpdatedAt = DateTime.UtcNow;
            customer.NameAr = request.NameAr;
            customer.NameEn = request.NameEn;
            customer.Code = request.Code;
            customer.BusinessId = request.BusinessId;
            customer.CountryId = request.CountryId;
            customer.CompanySize = request.CompanySize;
            customer.MultiBranches = request.MultiBranches;
            customer.MultiCompanies = request.MultiCompanies;
            customer.NumberOfBranch = request.NumberOfBranch;
            customer.NumberOfCompany = request.NumberOfCompany;
            customer.UpdateBy = _auditService.UserId;

            await _context.UpdateAsync(customer);
            await _context.SaveChangesAsync();
            return _mapper.Map<CustomerDto>(customer);
        }
        public async Task<CustomerSubscriptionDto> EditCustomerSubscriptionCommand(EditCustomerSubscriptionCommand request)
        {
            var customerSubscription = _mapper.Map<CustomerSubscription>(request.InputDto);

            var entityDb = await _contextCustomerSubscription.FirstOrDefaultAsync(c => c.Id == request.InputDto.Id);

            entityDb = _mapper.Map(request.InputDto, entityDb);


            await _contextCustomerSubscription.UpdateAsync(entityDb);
            await _contextCustomerSubscription.SaveChangesAsync();

            return _mapper.Map<CustomerSubscriptionDto>(customerSubscription);


        }
        public async Task<string> VerifyCodeCommand(VerifyCodeCommand request)
        {
            var customer = await _context.FirstOrDefaultAsync(u => u.Email == request.Email && !u.IsDeleted);

            if (customer == null)
                throw new UserFriendlyException("User not found ");

            var userCodes = await _context.FirstOrDefaultAsync(u => u.VerifyCode == request.Code && !u.IsDeleted && !(bool)customer.IsVerifyCode);

            if (userCodes == null)
                throw new UserFriendlyException("This code is not found or expire ");
            var subscription = _contextCustomerSubscription.GetAllList(c => c.CustomerId == customer.Id).LastOrDefault();
            var subscriptionDto = new SubscriptionDto()
            {
                Applications = subscription.Applications,
                ContractEndDate = subscription.ContractEndDate,
                ContractStartDate = subscription.ContractStartDate,
                Id = subscription.Id,
                NumberOfBranch = customer.NumberOfBranch,
                NumberOfCompany = customer.NumberOfCompany,
                MultiBranches = customer.MultiBranches,
                CreatedAt = DateTime.Now,
                MultiCompanies = customer.MultiCompanies
            };
            DatabaseHelper.DatabaseCreate(customer.ServerName, customer.DatabaseName, subscriptionDto, true, "sa", "123456");
            //DatabaseHelper.DatabaseCreate(customer.ServerName, "ERP1", subscriptionDto, true, "sa", "123456");

            string verificationLink = "";
            try
            {


                verificationLink = $"{customer?.SubDomain}/authentication/login";
                string message = EmailTemplates.GoToApplication(customer.NameAr, verificationLink, customer.Email);
                _emailHleper.SendEmailAsync(customer.Email, message, "الانتقال الي التطبيق");
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("probleminSenMail");
            }
            customer.IsVerifyCode = true;

            await _context.UpdateAsync(customer);
            await _context.SaveChangesAsync();
            return verificationLink;


        }
        public async Task<List<CustomerDto>> GetAllCustomersQuery()
        {
            var CustomerList = await _context.GetAllListAsync(x => !x.IsDeleted);

            return _mapper.Map<List<CustomerDto>>(CustomerList);

        }
        public async Task<PaginatedList<CustomerDto>> GetAllCustomersWithPaginationCommand(GetAllCustomersWithPaginationCommand request)
        {
            var query = _context.GetAllAsNoTracking().Where(c => !c.IsDeleted);

            if (!String.IsNullOrEmpty(request.Filter))
            {
                query = query.Where(r => r.NameAr.Contains(request.Filter));
            }

            var entities = query.Skip((request.PageIndex - 1) * request.PageSize)
                 .Take(request.PageSize).OrderBy(c => c.Code).ToList();

            var totalCount = await query.CountAsync();

            var transferReasonDto = _mapper.Map<List<CustomerDto>>(entities);

            return new PaginatedList<CustomerDto>(transferReasonDto,
                totalCount,
                request.PageIndex,
                request.PageSize);

        }
        public async Task<CustomerSubscriptionDto> GetByCustomerSubscriptionId(GetByCustomerSubscriptionId request)
        {
            var customerSubscription = await _contextCustomerSubscription.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == request.Id);

            return _mapper.Map<CustomerSubscriptionDto>(customerSubscription);

        }
        public async Task<CustomerDto> GetCustomer(CustomerSubDomain request)
        {
            var customerSubscription = await _context.GetAllIncluding(c => c.CustomerSubscriptions).FirstOrDefaultAsync(x => !x.IsDeleted && x.SubDomain == request.SubDomain);

            return _mapper.Map<CustomerDto>(customerSubscription);

        }

        public async Task<CustomerDto> GetByCustomerId(GetByCustomerId request)
        {
            var customer = await _context.GetAllIncluding(c => c.CustomerSubscriptions).Where(x => !x.IsDeleted && x.Id == request.Id).FirstOrDefaultAsync();
            var res = _mapper.Map<CustomerDto>(customer);
            res.SubscriptionDto = _mapper.Map<List<CustomerSubscriptionDto>>(customer.CustomerSubscriptions.Where(c => !c.IsDeleted));
            return res;

        }
        public async Task<string> GetLastCode()
        {
            var lastCode = await _context.GetAll().OrderByDescending(c => c.Id).FirstOrDefaultAsync(x => !x.IsDeleted);
            var code = GenerateRandom.GetSerialCode(Convert.ToInt64(lastCode != null ? lastCode.Code : "0"));
            _auditService.Code = code;
            return code;

        }


        //public void CreateDatabase(string databaseName)
        //{
        //    try
        //    {
        //        var connectionString = "Server=WEBSERVER\\SQLEXPRESS;User Id=sa;Password=Gmtcc@2001;Database=" + databaseName + ";Trusted_Connection=false;TrustServerCertificate=Yes;";

        //        var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();
        //        optionsBuilder.UseSqlServer(connectionString);

        //        using (var dbContext = new ErpDbContext(optionsBuilder.Options))
        //        {
        //            // Apply migrations
        //            dbContext.Database.Migrate();

        //            // Initialize the database (e.g., seed data)
        //            var logger = _serviceProvider.GetRequiredService<ILogger<SeedSQLScripts>>();
        //            AppDbInitializer.Initialize(dbContext, logger);

        //            Console.WriteLine($"Database for customer '{databaseName}' created successfully.");

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error creating database '{databaseName}': {ex.Message}");
        //        // Log the exception or handle it appropriately
        //    }
        //}

    
        public async Task CreateSubdomain(string _subdomain)
        {
            string apiKey = "gHzfH48Cv1PZ_KFF1rJmj4KBCGMrT4TvjLm";
            string apiSecret = "S65qiwAN7RR2f9r5UuXQBD";
            string domain = "afzaz.com";
            string subdomain = _subdomain;
            using (var httpClient = new HttpClient())
            {
                // Set up basic authentication using API key and secret
                var authValue = new AuthenticationHeaderValue("sso-key", $"{apiKey}:{apiSecret}");
                httpClient.DefaultRequestHeaders.Authorization = authValue;


                // Define the API endpoint for adding a DNS record
                string endpoint = $"https://api.godaddy.com/v1/domains/{domain}/records";

                // Prepare the DNS record payload for the subdomain
                string recordPayload = $@"[
                    {{
                        ""type"": ""A"",
                        ""name"": ""{subdomain}"",
                        ""data"": ""213.165.245.220"",
                        ""ttl"": 3600
                    }}
                ]";

                // Send a POST request to the GoDaddy API to add the DNS record
                var response = await httpClient.PatchAsync(endpoint, new StringContent(recordPayload, Encoding.UTF8, "application/json"));

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Handle success
                    //throw new UserFriendlyException("Sub Domain is done");
                    Console.WriteLine($"Success to create subdomain. Status code: {response.StatusCode}");

                }
                else
                {
                    // Handle failure (check response.Content for details)
                    Console.WriteLine($"Failed to create subdomain. Status code: {response.StatusCode}");
                    //throw new UserFriendlyException("Failed to create subdomain");

                }
            }
        }



    }
}
