using Common.SharedDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;
using ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Users
{
    public interface IUserDetailsDatabaseRepository 
    {
        Task<bool> CreateDatabaseAsync(SettingDataBaseDto setting);


    }

    public class UserDetailsDatabaseRepository: IUserDetailsDatabaseRepository
    {
            private readonly ILogger<SeedSQLScripts> _logger;
            private readonly DbContextOptions<ErpDbContext> _dbContextOptions;

            public UserDetailsDatabaseRepository(ILogger<SeedSQLScripts> logger, DbContextOptions<ErpDbContext> dbContextOptions)
            {
                _logger = logger;
                _dbContextOptions = dbContextOptions;
            }

            public async Task<bool> CreateDatabaseAsync(SettingDataBaseDto setting)
            {
                try
                {
                    var connectionString = $"Data Source=.;Initial Catalog={setting.OwnerInfoDto.NameDatabase};Integrated Security=True;TrustServerCertificate=Yes;";

                    var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();
                    optionsBuilder.UseSqlServer(connectionString);

                    using (var dbContext = new ErpDbContext(optionsBuilder.Options))
                    {
                        // Apply migrations
                        await dbContext.Database.MigrateAsync();

                        // Initialize the database (e.g., seed data)
                        AppDbInitializer.InitializeSeedingUserOwner(dbContext, _logger,setting);

                        _logger.LogInformation($"Database for customer '{setting}' created successfully.");
                    }

                    return true; // Database creation successful
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating database '{setting}': {ex.Message}");
                    return false; // Database creation failed
                }
            }
        private DbContextOptions<ErpDbContext> ConfigureDbContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ErpDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }
    }
    
}
