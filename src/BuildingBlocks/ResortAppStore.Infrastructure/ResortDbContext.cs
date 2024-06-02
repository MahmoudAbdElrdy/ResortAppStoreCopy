using Common.Extensions;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ResortDbContext : DbContext
    { //  public virtual IAppSession AppSession { get; }
        // public virtual IHttpContextAccessor HttpContextAccessor { get; }

        private static MethodInfo ConfigureGlobalFiltersMethodInfo =
            typeof(ResortDbContext).GetMethod(nameof(ConfigureGlobalFilters),
                BindingFlags.Instance | BindingFlags.NonPublic);
        //   public DbSet<AuditTable> AuditTable { get; set; }


        public ResortDbContext(DbContextOptions options)
            : base(options)
        {
            //  AppSession = session;
            //   if (context != null) HttpContextAccessor = context;
          
        }

        public override int SaveChanges()
        {
            ApplyAppChangesConcepts();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ApplyAppChangesConcepts();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder, entityType });
            }
        }

        protected virtual void ApplyAppChangesConcepts()
        {
            //  var userId = GetAuditUserId();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                ApplyAppChangesConcepts(entry, string.Empty);
            }

         
        }

     
        protected virtual void ApplyAppChangesConcepts(EntityEntry entry, string userId)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry, userId);
                    break;
                    //case EntityState.Modified:
                    //    ApplyConceptsForModifiedEntity(entry, userId);
                    //    break;
                    //case EntityState.Deleted:
                    //    ApplyConceptsForDeletedEntity(entry, userId);
                    //    break;
            }
        }

        protected virtual void ApplyConceptsForAddedEntity(EntityEntry entry, string userId)
        {
            // CheckAndSetId(entry);
            SetCreationTrackingProperties(entry.Entity, userId);
        }

        protected virtual void SetCreationTrackingProperties(object entryEntity, string userId)
        {
            if (!(entryEntity is IAudit creationEntity))
                return;
            if (creationEntity.CreatedDate == DateTime.MinValue)
            {
                creationEntity.CreatedDate = DateTime.UtcNow;
            }
        }

        protected virtual bool ShouldFilterEntity<TEntity>()
        {
            if (typeof(IAudit).IsAssignableFrom(typeof(TEntity)))
                return true;

            return false;
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

        protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class
        {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>())
            {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null)
                {
                    if (entityType.IsKeyless)
                    {
                        modelBuilder.Entity<TEntity>().HasNoKey().HasQueryFilter(filterExpression);
                    }
                    else
                    {
                        modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                    }
                }
            }
        }

    }
}
