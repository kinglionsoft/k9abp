using System.Linq;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Organizations;
using Abp.Runtime.Caching;

namespace K9Abp.Core.Authorization.Users
{
    public class UserCache: EntityCache<User, UserCacheItem, long>, IUserCache, ISingletonDependency
    {
        private IRepository<OrganizationUnit, long> OrganizationUnitRepository { get; set; }
        private IRepository<UserOrganizationUnit, long> UserOrganizationUnitRepository { get; set; }

        public UserCache(ICacheManager cacheManager, 
            IRepository<User, long> repository, 
            string cacheName = null) : base(cacheManager, repository, cacheName)
        {
        }

        protected override UserCacheItem MapToCacheItem(User entity)
        {
            var item = base.MapToCacheItem(entity);

            var ou = OrganizationUnitRepository.GetAllWithoutTracking()
                .Join(UserOrganizationUnitRepository.GetAllWithoutTracking(),
                    a => a.Id,
                    b => b.OrganizationUnitId,
                    (a, b) => new {a.Id, a.DisplayName})
                .FirstOrDefault();

            if (ou != null)
            {
                item.OrganizationUnitId = ou.Id;
                item.OrganizationUnitName = ou.DisplayName;
            }

            return item;
        }
    }
}