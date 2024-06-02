using Common;
using Common.Exceptions;
using Common.Extensions;
using System.Linq.Expressions;
using Common.Entity;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Z.EntityFramework.Plus;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections;
using System.Reflection;
using Common.Constants;
using Microsoft.Data.SqlClient;
using Sentry;
using System.Data.Common;
using System.Data;
using Web.Profiler;

namespace ResortAppStore.Repositories
{

    public class GRepository<TEntity> : IGRepository<TEntity>
   where TEntity : class
    {

        public virtual DbContext Context { get; set; }
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();


        public GRepository(DbContext context)
        {
            Context = context;

        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return Table;
        }
        public virtual IQueryable<TEntity> GetAllListMapp()
        {
            // return Table.Where(e => e.GetType().GetProperty("IsDeleted") != null && e.GetType().GetProperty("IsDeleted").GetValue(e).Equals(false));
            return Table.Where(u => EF.Property<bool>(u, "IsDeleted") == false);
        }
        public virtual IQueryable<TEntity> GetAllAsNoTracking()
        {
            return Table.AsNoTracking();
        }
        public virtual async Task<bool> CheckExistCodAsync(object code, object id)
        {


            if (code != null)
            {
              

                var existCode = await Table.AsNoTracking().AnyAsync(
                    (u => EF.Property<object>(u, "Id") == id && EF.Property<string>(u, "Code") == code && EF.Property<bool>(u, "IsDeleted") == false));

                if (existCode)
                    throw new UserFriendlyException("SameCodeExistsBefore");
            }
            return false;
        }
        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {

            if (propertySelectors.IsNullOrEmpty())
            {
                return GetAll();
            }

            var query = GetAll();

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }
        public virtual IQueryable<TEntity> GetAllIncluding(string includePath = "", Expression<Func<TEntity, bool>> filter = null)
        {
            var query = GetAll().Where(u => EF.Property<bool>(u, "IsDeleted") == false);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includePath))
            {
                var includePaths = includePath.Split(',');
                query = includePaths.Aggregate(query, (current, path) => current.Include(path.Trim()));
            }

            return query;
        }
        public virtual async Task<TResult> GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = GetAll();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public virtual async Task<List<TResult>> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = GetAll();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }

            return await query.Select(selector).ToListAsync();
        }

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().Where(u => EF.Property<bool>(u, "IsDeleted") == false  && EF.Property<bool>(u, "IsActive") == true).ToListAsync();
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //Expression<Func<TEntity, bool>> softDeleteFilter = e => !((IDeleteTrackingEntity)e).IsDeleted;
            //predicate = predicate == null ? softDeleteFilter : CombineExpressions(predicate, softDeleteFilter);
            return await GetAll().Where(predicate).ToListAsync();
        }
        protected virtual Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
         where TEntity : class
        {
            Expression<Func<TEntity, bool>> expression = null;

            if (typeof(IDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !((IDeleteEntity)e).IsDeleted;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }

            return expression;
        }

        protected virtual Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return ExpressionCombiner.Combine(expression1, expression2);
        }

        //public virtual TEntity Get(TKey id)
        //{
        //    var entity = FirstOrDefault(id);
        //    if (entity == null)
        //    {
        //        throw new EntityNotFoundException(typeof(TEntity), id);
        //    }

        //    return entity;
        //}

        //public virtual async Task<TEntity> GetAsync(TKey id)
        //{
        //    var entity = await FirstOrDefaultAsync(id);
        //    return entity;
        //}

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().SingleAsync(predicate);
        }

        //public virtual TEntity FirstOrDefault(TKey id)
        //{
        //    return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        //}

        //public virtual async Task<TEntity> FirstOrDefaultAsync(TKey id)
        //{
        //    return await GetAll().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        //}

        public virtual async Task<TEntity> FirstOrDefaultWithNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(object id)
        {
            return await Context.Set<TEntity>().Where(u => EF.Property<object>(u, "Id") == id && EF.Property<bool>(u, "IsDeleted") == false).FirstOrDefaultAsync();
        }
        public virtual TEntity FirstOrDefault(object id)
        {
            return Context.Set<TEntity>().Where(u => EF.Property<object>(u, "Id") == id && EF.Property<bool>(u, "IsDeleted") == false).FirstOrDefault();
        }
        public virtual TEntity LastCode()
        {
            var code=Context.Set<TEntity>().OrderByDescending(u => EF.Property<object>(u, "Id"))
                .Where(u => EF.Property<bool>(u, "IsDeleted") == false).FirstOrDefault();
            return code;
        }
        //public virtual TEntity Load(TKey id)
        //{
        //    return Get(id);
        //}

        public virtual TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var e = await Table.AddAsync(entity);
            return e.Entity;
        }


        public virtual IExecutionStrategy GetExecutionStrategy()
        {
            return Context.Database.CreateExecutionStrategy();
        }


        public virtual async Task<IDbContextTransaction> GetTransaction()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        public virtual async Task InsertAsync(List<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }



        public virtual TEntity Update(TEntity entity)
        {
            try
            {
                AttachIfNot(entity);
                Context.Entry(entity).State = EntityState.Modified;
                return entity;
            }
            catch(Exception ex)
            {
                return entity;
            }
           
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity = Update(entity);
            return Task.FromResult(entity);
        }


        public virtual async Task<int> UpdateBulkAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return await Table.Where(predicate).UpdateAsync(updateFactory);
        }

        //need license to work
        public virtual async Task UpdateBulkAsync(IEnumerable<TEntity> entities, int batchSize = 1000, bool returnIdentity = false)
        {
            await Table.BulkUpdateAsync<TEntity>(entities
                , options =>
                {
                    options.IncludeGraph = true;
                    options.BatchSize = batchSize;
                    options.AutoMapOutputDirection = returnIdentity;
                }
            );
        }

        public virtual async Task<int> DeleteBulkAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> predicateOver = null)
        {
            if (predicateOver != null)
                return await Table.Where(predicate).OrderByDescending(predicateOver).Skip(1).DeleteAsync();
            return await Table.Where(predicate).DeleteAsync();
        }
        public virtual void DeleteAll(IEnumerable<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        //need license to work
        public virtual async Task InsertBulkAsync(IList<TEntity> entities, int batchSize = 1000, bool returnIdentity = false)
        {
            await Table.BulkInsertAsync(entities
                , options =>
                {
                    options.IncludeGraph = true;
                    options.BatchSize = batchSize;
                    options.AutoMapOutputDirection = returnIdentity;
                }
            );
        }
        public virtual async Task InsertRangeAsync(IList<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            //  AttachIfNot(entity);
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Table.Remove(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Delete(entity);
            return Task.FromResult(0);
        }
        public virtual Task SoftDeleteAsync(TEntity entity)
        {
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Update(entity);
            return Task.FromResult(0);
        }
        public Task<int> SoftDeleteAsync(object id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            PropertyInfo propertyInfo = entity.GetType().GetProperty("IsDeleted");

            // Set the value of the property
            propertyInfo.SetValue(entity, true);
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Update(entity);
            Context.SaveChanges();
            return Task.FromResult(0);
        }
        public Task<int> SoftDeleteWithoutSaveAsync(object id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            PropertyInfo propertyInfo = entity.GetType().GetProperty("IsDeleted");

            // Set the value of the property
            propertyInfo.SetValue(entity, true);
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Update(entity);
           // Context.SaveChanges();
            return Task.FromResult(0);
        }
        public Task SoftDeleteListAsync(object id) 
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }
            PropertyInfo propertyInfo = entity.GetType().GetProperty("IsDeleted");

            // Set the value of the property
            propertyInfo.SetValue(entity, true);
            var deletedAtProperty = entity.GetType().GetProperty("DeletedAt");

            if (deletedAtProperty != null)
            {
                deletedAtProperty.SetValue(entity, DateTime.Now);
            }
            var deletedBy = entity.GetType().GetProperty("DeletedBy");

            if (deletedBy != null)
            {
                deletedBy.SetValue(entity, ServiceLocator.GetAuditService().UserId);
            }
            Update(entity);
          
            return Task.FromResult(0);
        }

        public virtual void SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Update(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //  Delete(predicate);
            return Task.FromResult(0);
        }

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await GetAll().CountAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().CountAsync(predicate);
        }





        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LongCount(predicate);
        }

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().LongCountAsync(predicate);
        }

        public virtual int SaveChanges(bool detectChanges = true)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = detectChanges;

            var result = Context.SaveChanges();

            return result;
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken(), bool detectChanges = true)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = detectChanges;
            await SaveChangesAudit(cancellationToken);
            var result = await Context.SaveChangesAsync(cancellationToken);

            return result;
        }
        private async Task SaveChangesAudit(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in Context.ChangeTracker.Entries<IAudit>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }
            //   return SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> FromSqlAsync(string sql)
        {
            return await Context.Database.ExecuteSqlRawAsync(sql);
        }



        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }
        public ResponseResult Excute(string query, List<SqlParameter> parameters, bool isSp)
        {
            try
            {
                Context.Database.SetCommandTimeout(0);
                DbConnection cn = Context.Database.GetDbConnection();
                using (DbCommand cmd = cn.CreateCommand())
                {
                    if (isSp)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    cmd.CommandText = query;

                    if (cn.State.Equals(ConnectionState.Closed))
                    {
                        cn.Open();
                    }

                    if (parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }


                    cmd.ExecuteNonQuery();



                }
                if (cn.State.Equals(ConnectionState.Open))
                {
                    cn.Close();
                }

                return new ResponseResult()
                {
                    Success = true,
                    Message = "success"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }

        }
        public ResponseResult<List<T>> Excute<T>(string query, List<SqlParameter> parameters, bool isSp)
        {
            try
            {
                Context.Database.SetCommandTimeout(0);
                DbConnection cn = Context.Database.GetDbConnection();
                using (DbCommand cmd = cn.CreateCommand())
                {
                    if (isSp)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    cmd.CommandText = query;

                    if (cn.State.Equals(ConnectionState.Closed))
                    {
                        cn.Open();
                    }

                    if (parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }

                    List<T> resultList = new List<T>();

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T result = Activator.CreateInstance<T>(); // Create a new instance of T

                            // Populate the properties of the result object using values from the reader
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                var propertyName = reader.GetName(i);
                                var property = typeof(T).GetProperty(propertyName);

                                if (property != null && !reader.IsDBNull(i))
                                {
                                    var value = reader.GetValue(i);

                                    if (property.PropertyType == typeof(int?) && value is long longValue)
                                    {
                                        value = (int?)longValue; // Convert long to int?
                                    }

                                    property.SetValue(result, value);
                                }
                            }

                            resultList.Add(result);
                        }
                    }

                    if (cn.State.Equals(ConnectionState.Open))
                    {
                        cn.Close();
                    }

                    return new ResponseResult<List<T>>()
                    {
                        Success = true,
                        Message = "success",
                        Data = resultList
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<T>>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
        public ResponseResult<IEnumerable<TEntity>> Query<TEntity>(string query, List<SqlParameter> parameters, bool isSp) where TEntity : class
        {
            try
            {
                Context.Database.SetCommandTimeout(0);
                List<TEntity> result = new List<TEntity>();
                DbConnection cn = Context.Database.GetDbConnection();
                using (DbCommand cmd = cn.CreateCommand())
                {
                    if (isSp)
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    cmd.CommandText = query;

                    if (cn.State.Equals(ConnectionState.Closed))
                    {
                        cn.Open();
                    }

                    if (parameters.Count > 0)
                    {
                        cmd.Parameters.AddRange(parameters.ToArray());
                    }


                    var reader = cmd.ExecuteReader();
                    result = ConvertDataReaderToList<TEntity>(reader);
                    if (cn.State.Equals(ConnectionState.Open))
                    {
                        cn.Close();
                    }

                    return new ResponseResult<IEnumerable<TEntity>>()
                    {
                        Data = result,
                        Message = "success",
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult<IEnumerable<TEntity>>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = new List<TEntity>()
                };
            }


        }

        public List<TEntity> ConvertDataReaderToList(DbDataReader dr)
        {
            string className = typeof(TEntity).FullName;
            string assemblyName = typeof(TEntity).Assembly.GetName().Name;
            PropertyInfo[] properties = Type.GetType(className + "," + assemblyName).GetProperties();
            List<TEntity> data = new List<TEntity>();
            var entityType = Context.Model.FindEntityType(typeof(TEntity));
            while (dr.Read())
            {
                TEntity obj = (TEntity)Activator.CreateInstance(typeof(TEntity));
                foreach (PropertyInfo p in properties)
                {
                    try
                    {
                        var property = entityType.GetProperties().Single(property => property.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                        var columnName = property.GetColumnName();

                        //var s = dr[p.Name];

                        if (dr[columnName].Equals(DBNull.Value))
                        {
                            obj.GetType().GetProperty(p.Name).SetValue(obj, null);
                        }
                        else
                        {
                            obj.GetType().GetProperty(p.Name).SetValue(obj, dr[columnName], null);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //obj = null;
                    }


                    //d[p.Name] = dr[p.Name];
                }

                data.Add(obj);
            }
            dr.Close();

            return data;
        }

        public List<TEntity> ConvertDataReaderToList<TEntity>(DbDataReader dr) where TEntity : class
        {
            string className = typeof(TEntity).FullName;
            string assemblyName = typeof(TEntity).Assembly.GetName().Name;
            PropertyInfo[] properties = Type.GetType(className + "," + assemblyName).GetProperties();
            List<TEntity> data = new List<TEntity>();
            var entityType = Context.Model.FindEntityType(typeof(TEntity));
            while (dr.Read())
            {
                TEntity obj = (TEntity)Activator.CreateInstance(typeof(TEntity));
                foreach (PropertyInfo p in properties)
                {
                    try
                    {
                        var property = entityType.GetProperties().Single(property => property.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                        var columnName = property.GetColumnName();

                        //var s = dr[p.Name];

                        if (dr[columnName].Equals(DBNull.Value))
                        {
                            obj.GetType().GetProperty(p.Name).SetValue(obj, null);
                        }
                        else
                        {
                            obj.GetType().GetProperty(p.Name).SetValue(obj, dr[columnName], null);
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        //obj = null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    //d[p.Name] = dr[p.Name];
                }

                data.Add(obj);
            }
            dr.Close();

            return data;
        }



    }
}
