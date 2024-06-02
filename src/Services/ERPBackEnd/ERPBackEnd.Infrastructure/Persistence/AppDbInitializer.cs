using Common.SharedDto;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;
using System.Linq;

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence 
{
  public partial class AppDbInitializer {
       
    public AppDbInitializer() {

    }
    public static void Initialize(ErpDbContext context,ILogger<SeedSQLScripts> logger) {
      var initializer = new AppDbInitializer();
     initializer.SeedAuthEverything(context);

         var seedSqlServerScript = new SeedSQLScripts(logger);
         seedSqlServerScript.SeedSqlScriptsAfterMigration(context);

            
    }
        public static void InitializeSeedingUserOwner(ErpDbContext context, ILogger<SeedSQLScripts> logger,SettingDataBaseDto settingDataBaseDto)
        {
            var initializer = new AppDbInitializer();
            initializer.SeedUserOwner(context,settingDataBaseDto);

            //var seedSqlServerScript = new SeedSQLScripts(logger);
            //seedSqlServerScript.SeedSqlScriptsAfterMigration(context);


        }
    }

}