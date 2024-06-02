using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;
using ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence;
using Common.SharedDto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Users;
using Common.Constants;
using ResortAppStore.Repositories;

namespace  ResortAppStore.Services.ERPBackEnd.API.Controllers.Configuration.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingErpController : ControllerBase
    {

        private readonly ILogger<SeedSQLScripts> _logger;
        private readonly IUserDetailsDatabaseRepository _userDetailsDatabaseRepository;
      
        public SettingErpController( ILogger<SeedSQLScripts> logger, IUserDetailsDatabaseRepository userDetailsDatabaseRepository)
        {
        
            _logger = logger;
            _userDetailsDatabaseRepository = userDetailsDatabaseRepository;
        }

       
        [HttpPost("create-database")]
        public async Task<Response> CreateDatabase([FromBody] SettingDataBaseDto setting)
        {
            try
            {
                var result = await _userDetailsDatabaseRepository.CreateDatabaseAsync(setting);
              
                if (result)
                {
                 return   new Response() { Message = "Database created successfully.", Status = "200", Success = true};
                
                }
                else
                {
                   
                return    new Response() { Message = "Error creating database.", Status = "500", Success = false };
                }


            }
            catch (Exception ex)
            {

                return new Response() { Message = "Error creating database.", Status = "500", Success = false };
            }
          
        }

    }
}
