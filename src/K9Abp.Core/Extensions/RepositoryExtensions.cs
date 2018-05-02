using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Abp.Domain.Repositories
{
    public static class RepositoryExtensions
    {

        public static IQueryable<TEntity> GetAllWithoutTracking<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository) where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetAll().AsNoTracking();
        }

        public static IQueryable<TEntity> GetAllWithoutTracking<TEntity>(this IRepository<TEntity, int> repository) where TEntity : class, IEntity<int>
        {
            return repository.GetAll().AsNoTracking();
        }

        public static Task<TEntity> FirstOrDefaultWithoutTrackingAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, 
            [NotNull] Expression<Func<TEntity, bool>> predicate, 
            CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetAll().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public static Task<TEntity> FirstOrDefaultWithoutTrackingAsync<TEntity>(this IRepository<TEntity, int> repository,
            [NotNull] Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class, IEntity<int>
        {
            return repository.GetAll().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public static Task<TEntity> FirstOrDefaultWithoutTrackingAsync<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository, 
            CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class, IEntity<TPrimaryKey>
        {
            return repository.GetAll().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public static Task<TEntity> FirstOrDefaultWithoutTrackingAsync<TEntity>(this IRepository<TEntity, int> repository,
            CancellationToken cancellationToken = default(CancellationToken))
            where TEntity : class, IEntity<int>
        {
            return repository.GetAll().AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }
    }
}