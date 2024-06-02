using Common.Extensions;
using CRM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Services
{
    public static class DatabaseHelper
    {
        static IWebHostEnvironment _hostingEnvironment;
       

        public static void Initialize(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(DatabaseHelper)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
        public static void DatabaseCreate(string serverName, string databaseName, SubscriptionDto input, bool trusted_Connection = true, string userId = "", string passWord = "")
        {
            var sqlConnectionString = "";

            if (trusted_Connection)
            {
                sqlConnectionString = $"Server={serverName};Initial Catalog={databaseName};Integrated Security={trusted_Connection};TrustServerCertificate=Yes";

            }
            else
            {
                sqlConnectionString = $"Server={serverName};Initial Catalog={databaseName};User Id={userId};Password={passWord};Integrated Security={trusted_Connection};";

            }
            SqlConnectionStringBuilder connBuilder = new()
            {
                ConnectionString = sqlConnectionString
            };

            string dbName = connBuilder.InitialCatalog;

            var masterConnection = sqlConnectionString.Replace(dbName, "master");

            using (SqlConnection connection = new(masterConnection))
            {
                connection.Open();
                using var checkIfExistsCommand = new SqlCommand($"SELECT * FROM dbo.sysdatabases WHERE name = '{dbName}'", connection);
                var result = checkIfExistsCommand.ExecuteScalar();

                SqlConnection connectiondbName = new(sqlConnectionString);
                if (result == null)
                {
                    var command = $"CREATE DATABASE \"{dbName}\"";

                    connection.Execute(command);
                    //connection.Close();
                  //  connectiondbName.Open();

                   // connectiondbName.Execute(createTables(dbName,input));
                  
                   // Insert(input, sqlConnectionString);

                    connectiondbName.Close();


                   
                }
                else
                {
                    var command = $"Drop DATABASE \"{dbName}\"";

                    connection.Execute(command);
                    throw new UserFriendlyException("nameDataBaseCreateBefor");
                }

            }
        }
        private static string _server;

        public static string createTables(string databaseName,SubscriptionDto subscriptionDto)
        {
            if (_server == null)
            {
                //var stream = typeof(DatabaseHelper).GetTypeInfo().Assembly.GetManifestResourceStream("Nashmi.Services.Ordering.API.DatabaseHelper.QuartzDatabase.sql");

                //using var reader = new StreamReader(stream);

                //_postgreServer = reader.ReadToEnd();

                _server = ReadPhysicalFile("/wwwroot/SeedData/InitialInsertData.sql");


                _server = "USE " + databaseName + _server;
               
                //string query = $"INSERT INTO dbo.Subscriptions (id,applications,contractStartDate,contractEndDate,numberOfBranch,numberOfCompany,createdAt) " +
                //" VALUES ('" + subscriptionDto.Id + "', '" + subscriptionDto.Applications + "', '" + subscriptionDto.ContractStartDate + "', '" + subscriptionDto.ContractEndDate + "', '" + subscriptionDto.NumberOfBranch??0 + "', '" + subscriptionDto.NumberOfCompany + "','"+DateTime.UtcNow.ToString()+"); ";
                ////  .Replace("{ERP1}", databaseName);
                //_server = _server + query;

            }

            return _server;
        }
        public static void Insert(SubscriptionDto input,string _connectionString)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                
                string query =$"INSERT INTO dbo.Subscriptions (id,applications,contractStartDate,contractEndDate,numberOfBranch,numberOfCompany,multiCompanies,createdAt,isActive,isDeleted) " +
                    "VALUES (@id,@applications,@contractStartDate,@contractEndDate, @numberOfBranch, @numberOfCompany,@multiCompanies,@createdAt,@isActive,@isDeleted)";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", input.Id);
                    command.Parameters.AddWithValue("@applications", input.Applications);
                    command.Parameters.AddWithValue("@contractStartDate", input.ContractStartDate);
                    command.Parameters.AddWithValue("@contractEndDate", input.ContractEndDate);
                    command.Parameters.AddWithValue("@numberOfBranch", ((object)input.NumberOfBranch) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@multiCompanies", ((object)input.MultiCompanies) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@numberOfCompany", ((object)input.NumberOfCompany) ?? DBNull.Value);
                    command.Parameters.AddWithValue("@createdAt", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@isActive", true);
                    command.Parameters.AddWithValue("@isDeleted", false);

                  ;
                    
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
        }
        private static void Execute(this IDbConnection conn, string sql)
        {
            var command = conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
    }
}
