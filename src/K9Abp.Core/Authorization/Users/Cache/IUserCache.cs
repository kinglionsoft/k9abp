using Abp.Domain.Entities.Caching;

namespace K9Abp.Core.Authorization.Users
{
    public interface IUserCache: IEntityCache<UserCacheItem, long>
    {
        
    }
}