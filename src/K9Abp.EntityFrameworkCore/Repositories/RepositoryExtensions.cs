using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Castle.DynamicProxy;
using K9Abp.EntityFrameworkCore.Repositories;

namespace Abp.Domain.Repositories
{
    public static class RepositoryExtensions
    {
        private static K9AbpRepositoryBase<TEntity, TPrimaryKey> GetUnproxiedType<TEntity, TPrimaryKey>(
            this IRepository<TEntity, TPrimaryKey> repository)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            if (repository is K9AbpRepositoryBase<TEntity, TPrimaryKey> repositoryBase)
            {
                return repositoryBase;
            }
            var result = ProxyUtil.GetUnproxiedInstance(repository) as K9AbpRepositoryBase<TEntity, TPrimaryKey>;
            if (result != null)
            {
                return result;
            }
            throw new NotSupportedException();
        }

        public static Task ExecuteSqlAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
            string sql, IEnumerable<object> parameters,
            CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetUnproxiedType().ExecuteSqlAsync(sql, parameters, token);
        }

        public static Task BulkInsertAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetUnproxiedType().BulkInsertAsync(entities, token);
        }

        public static Task BulkUpdateAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities, CancellationToken token = default)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetUnproxiedType().BulkUpdateAsync(entities, token);
        }
    }
}