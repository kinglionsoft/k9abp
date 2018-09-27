using System.Threading.Tasks;
using K9Abp.Application.Authorization;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    public class HomeController: BroadbandControllerBase
    {
        private readonly SignInManager _signInManager;
        private readonly LogInManager _logInManager;

        public HomeController(SignInManager signInManager, LogInManager logInManager)
        {
            _signInManager = signInManager;
            _logInManager = logInManager;
        }

        [HttpGet("/broadband")]
        [HttpGet("/broadband/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}