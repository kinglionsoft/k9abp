using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities;

namespace Abp.Domain.Repositories
{
    public interface IBulkRepository<TEntity, TPrimaryKey>:
        IRepository<TEntity, TPrimaryKey>, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
    {
        Task BulkInsertAsync(IEnumerable<TEntity> entities, CancellationToken token = default);
        
        Task BulkUpdateAsync(IEnumerable<TEntity> entities, CancellationToken token = default);
    }

    public interface IBulkRepository<TEntity> :
        IBulkRepository<TEntity, int>, ITransientDependency
        where TEntity : class, IEntity<int>
    {
    }
}