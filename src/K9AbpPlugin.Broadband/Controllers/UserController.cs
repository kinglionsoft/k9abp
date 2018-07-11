using System.Threading.Tasks;
using K9Abp.Application.Authorization.Users;
using K9AbpPlugin.Broadband.User;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    /// <summary>
    /// Views of broadband users
    /// </summary>
    public class UserController: BroadbandControllerBase
    {
        private readonly IBroadbandAppService _broadbandAppService;

        public UserController(IBroadbandAppService broadbandAppService)
        {
            _broadbandAppService = broadbandAppService;
        }

        /// <summary>
        /// Show detail of broadband user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(int id)
        {
            var user = await _broadbandAppService.GetAsync(id);
            return View(user);
        }
    }
}