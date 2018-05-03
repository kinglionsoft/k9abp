using Abp.Dependency;
using K9Abp.Core.Authorization.Users;

namespace Abp.Runtime.Session
{
    public static class AbpSessionExtensions
    {
        public static UserCacheItem GetCurrentUser(this IAbpSession session)
        {
            if (session.UserId == null) return null;
            var useCache = IocManager.Instance.Resolve<IUserCache>();
            return useCache.Get(session.UserId.Value);
        }
    }
}