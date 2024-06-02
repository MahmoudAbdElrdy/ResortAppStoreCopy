using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Users
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string connection);
        void SetConnectionString(string connectionString);
    }

    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString;

        public string GetConnectionString(string connection)
        {
            //  _connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connection == null)
                return "ResortERP_Test4";
            return _connectionString;
            
        }

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }

}
