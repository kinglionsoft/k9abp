using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public class K9AbpRepositoryBase<TEntity, TPrimaryKey> 
        : EfCoreRepositoryBase<K9AbpDbContext, TEntity, TPrimaryKey>,
            IRepository<TEntity, TPrimaryKey>,
            IBulkRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public K9AbpRepositoryBase(IDbContextProvider<K9AbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //TODO:  Add your common methods for all repositories

        public virtual Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                foreach (var entity in entities)
                {
                    Context.Entry(entity).State = EntityState.Added;
                }

                return UnitOfWorkManager.Current.SaveChangesAsync();
            }
            finally
            {
                Context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        public virtual Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="K9AbpRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class K9AbpRepositoryBase<TEntity> : K9AbpRepositoryBase<TEntity, int>, IRepository<TEntity>, IBulkRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public K9AbpRepositoryBase(IDbContextProvider<K9AbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}

