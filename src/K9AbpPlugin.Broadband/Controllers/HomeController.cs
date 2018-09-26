using System.Threading.Tasks;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    public class HomeController: BroadbandControllerBase
    {
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;

        public HomeController(SignInManager signInManager, UserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("/broadband")]
        [HttpGet("/broadband/home")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Test()
        {
            var user = await _userManager.FindByIdAsync("10");
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index");
        }
    }
}