using Abp.Domain.Entities.Caching;

namespace Abp.Organizations
{
    public interface IOrganizationUnitCache: IEntityCache<OrganizationUnitCacheItem, long>
    {
        
    }
}