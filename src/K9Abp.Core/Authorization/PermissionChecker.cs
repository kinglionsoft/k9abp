using Abp.Authorization;
using K9Abp.Core.Authorization.Roles;
using K9Abp.Core.Authorization.Users;

namespace K9Abp.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

