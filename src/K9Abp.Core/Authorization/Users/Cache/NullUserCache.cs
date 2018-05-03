using System.Threading.Tasks;
using Abp.Runtime.Caching;

namespace K9Abp.Core.Authorization.Users
{
    public class NullUserCache: IUserCache
    {
        private NullUserCache()
        {
        }

        private static NullUserCache _instance;

        public static NullUserCache Instance => _instance ?? (_instance = new NullUserCache());

        public UserCacheItem Get(long id)
        {
           return new UserCacheItem();
        }

        public Task<UserCacheItem> GetAsync(long id)
        {
            return Task.FromResult(new UserCacheItem());
        }

        public UserCacheItem this[long id] => new UserCacheItem();

        public string CacheName { get; }
        public ITypedCache<long, UserCacheItem> InternalCache { get; }
    }
}