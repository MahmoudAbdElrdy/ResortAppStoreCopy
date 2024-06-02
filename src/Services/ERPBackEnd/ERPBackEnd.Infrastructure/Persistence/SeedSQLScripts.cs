using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResortAppStore.Services.ErpBackEnd.Infrastructure.Persistence;

namespace ResortAppStore.Services.ERPBackEnd.Infrastructure.Persistence
{
    public class SeedSQLScripts
    {
        
        private readonly ILogger<SeedSQLScripts> _logger;
        public SeedSQLScripts(ILogger<SeedSQLScripts> logger)
        {
            _logger=logger;
        }

       

        public virtual void SeedSqlScriptsAfterMigration(ErpDbContext context)
        {
            try
            {
                string sqlObjtctName = "";
                string fileScriptContent;
                string path = @"wwwroot/DbScript";
                string[] files = Directory.GetFiles(path + Path.DirectorySeparatorChar, "*.txt");
  
                foreach (var file in files)
                {
                    try
                    {
                        sqlObjtctName = Path.GetFileNameWithoutExtension(file);
                        fileScriptContent = File.ReadAllText(path + Path.DirectorySeparatorChar + sqlObjtctName + ".txt");
                       
                        if(fileScriptContent!="")
                        {
                            context.Database.ExecuteSqlRaw(fileScriptContent);
                        }
                    }
                    catch (Exception ex)
                   {
                        _logger.LogError(ex, "Error occurred during create Sql Script Object Name: " + sqlObjtctName + "Error Message: " + ex.Message);
                    }

                }
           
            }
            catch (Exception ex)
            {
              _logger.LogError(ex, "Error occurred during create Sql Script Object Name: "+ ex.Message);
            }
        }


        public void GetListOfScriptsFiles()
        {
            
           
            
         
        }
    }
}