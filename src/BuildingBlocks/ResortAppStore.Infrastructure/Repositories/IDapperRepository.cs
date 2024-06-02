using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
    public interface IDapperRepository<TEntity> where TEntity : class
    { 
        [NotNull]
        IEnumerable<TEntity> GetAll();

     
        Task<IEnumerable<TEntity>> GetAllAsync();

       
        [NotNull]
        IEnumerable<TEntity> Query([NotNull] string query, [CanBeNull] object parameters = null);
        IEnumerable<dynamic> Querydynamic(string query, object parameters = null);

        //[NotNull]
        //Task<IEnumerable<TEntity>> QueryAsync([NotNull] string query, [CanBeNull] object parameters = null);

        //[NotNull]
        //IEnumerable<TAny> Query<TAny>([NotNull] string query, [CanBeNull] object parameters = null) where TAny : class;


        Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null);
        //[NotNull]
        //Task<TAny> QueryFirstOrDefaultAsync<TAny>([NotNull] string query, [CanBeNull] object parameters = null);
        Task<TAny> QueryFirstOrDefault<TAny>(string query, object parameters = null);

        int Execute([NotNull] string query, [CanBeNull] object parameters = null);

       
        Task<int> ExecuteAsync([NotNull] string query, [CanBeNull] object parameters = null);
    }
}
