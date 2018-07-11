using System.Collections.Generic;
using System.Threading.Tasks;
using K9AbpPlugin.Broadband.User;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    public class ChannelController: BroadbandControllerBase
    {
        private readonly IBroadbandAppService _broadbandAppService;

        public ChannelController(IBroadbandAppService broadbandAppService)
        {
            _broadbandAppService = broadbandAppService;
        }

        public async Task<IActionResult> Warn(int distinctId)
        {
            var model = await _broadbandAppService.GetWarnUsersAsync(distinctId);
            return View(model);
        }
    }
}