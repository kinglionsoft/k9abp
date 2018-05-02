using System.Linq;
using Abp.EntityFrameworkCore;
using K9Abp.Core.Authorization.Users;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.EntityFrameworkCore.Repositories
{
    public class UserRepository: K9AbpRepositoryBase<User, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<K9AbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public string GetUserName(long userId)
        {
            return GetAll().AsNoTracking()
                .Where(x => x.Id == userId)
                .Select(x => x.Name)
                .FirstOrDefault();
        }
    }
}