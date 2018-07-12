using System.Linq;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Microsoft.EntityFrameworkCore;

namespace Abp.Organizations
{
    public class OrganizationUnitCache: EntityCache<OrganizationUnit, OrganizationUnitCacheItem, long>,
        IOrganizationUnitCache, ITransientDependency
    {
        private readonly IRepository<DistinctOrganizationUnit, long> _distinctOuRepository;
        public OrganizationUnitCache(ICacheManager cacheManager, IRepository<OrganizationUnit, long> repository, IRepository<DistinctOrganizationUnit, long> distinctOuRepository, string cacheName = null) : base(cacheManager, repository, cacheName)
        {
            _distinctOuRepository = distinctOuRepository;
        }

        protected override OrganizationUnitCacheItem MapToCacheItem(OrganizationUnit entity)
        {
            var item = base.MapToCacheItem(entity);
            var distinct = _distinctOuRepository.GetAllIncluding(
                    x => x.Distinct.County)
                .AsNoTracking()
                .FirstOrDefault(x => x.OrganizationUnitId == entity.Id);
            if (distinct != null)
            {
                item.DistinctId = distinct.DistinctId;
                item.DistinctName = distinct.Distinct.Name;
                item.CountyId = distinct.Distinct.County.Id;
                item.CountyName = distinct.Distinct.County.Name;
            }
            return item;
        }
    }
}