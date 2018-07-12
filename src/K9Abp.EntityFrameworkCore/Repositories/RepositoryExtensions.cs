using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using K9Abp.EntityFrameworkCore.Repositories;

namespace Abp.Domain.Repositories
{
    public static class RepositoryExtensions
    {
        public static Task ExecuteSqlAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
            string sql, IEnumerable<object> parameters,
            CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return (repository as K9AbpRepositoryBase<TEntity, TPrimaryKey>).ExecuteSqlAsync(sql, parameters, token);
        }

        public static Task BulkInsertAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return (repository as K9AbpRepositoryBase<TEntity, TPrimaryKey>).BulkInsertAsync(entities, token);
        }

        public static Task BulkUpdateAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, 
            IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return (repository as K9AbpRepositoryBase<TEntity, TPrimaryKey>).BulkUpdateAsync(entities, token);
        }
    }
}