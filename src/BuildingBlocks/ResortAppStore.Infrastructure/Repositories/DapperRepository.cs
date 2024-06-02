using Common.Interfaces;
using Dapper;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace Common.Repositories
{
    public class DapperRepository<TEntity> : IDapperRepository<TEntity> where TEntity : class
    {
        private readonly IConfiguration _configuration;
        private readonly IAuditService _auditService;
        private readonly string _connectionString;
        public DapperRepository(IConfiguration configuration, IAuditService auditService)
        {
            _configuration = configuration;
            _auditService = auditService;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        protected DbConnection OpenConnection()
        {
            SqlConnectionStringBuilder connBuilder = new()
            {
                ConnectionString = _connectionString
            };
            var masterConnection = _connectionString;
            string dbName = connBuilder.InitialCatalog;

            if (dbName == "{dbName}")
                masterConnection = _connectionString.Replace(dbName, "ERP1");

            var con = new SqlConnection(masterConnection);
            con.Open();
            return con;
        }
        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = OpenConnection())
            {
                return connection.GetList<TEntity>();
            }
        }

        public IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query<TEntity>(query, parameters);
            }

        }
        public IEnumerable<dynamic> Querydynamic(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Query<dynamic>(query, parameters);
            }

        }
        public IDbConnection CreateConnection()
     => new SqlConnection(_connectionString);

        public async Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return await connection.QueryAsync<TAny>(query, parameters);
            }
        }

        public async Task<TAny> QueryFirstOrDefault<TAny>(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<TAny>(query, parameters);
            }
        }

        public int Execute(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return connection.Execute(query, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = OpenConnection())
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }


    }
}
