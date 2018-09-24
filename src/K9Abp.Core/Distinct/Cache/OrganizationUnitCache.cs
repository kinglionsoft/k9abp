using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;

namespace Abp.Organizations
{
    public class OrganizationUnitCache : EntityCache<OrganizationUnit, OrganizationUnitCacheItem, long>,
        IOrganizationUnitCache, ITransientDependency
    {
        public OrganizationUnitCache(ICacheManager cacheManager, IRepository<OrganizationUnit, long> repository, string cacheName = null) 
            : base(cacheManager, repository, cacheName)
        {
        }

        protected override OrganizationUnitCacheItem MapToCacheItem(OrganizationUnit entity)
        {
            var item = base.MapToCacheItem(entity);

            if (entity.Parent != null)
            {
                item.Parent = base.MapToCacheItem(entity.Parent);
            }
            return item;
        }
    }
}