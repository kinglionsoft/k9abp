using Abp.AspNetCore.Mvc.Authorization;
using K9Abp.Core;
using K9Abp.Web.Core.Controllers;

namespace K9Abp.Web.Host.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(IAppFolders appFolders)
            : base(appFolders)
        {
        }
    }
}
