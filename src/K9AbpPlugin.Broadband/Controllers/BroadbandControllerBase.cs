using K9Abp.Core;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("broadband/[controller]/[action]")]
    public abstract class BroadbandControllerBase: K9AbpControllerBase
    {
        
    }
}