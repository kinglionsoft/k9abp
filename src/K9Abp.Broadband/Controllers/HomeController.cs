using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace K9Abp.Broadband.Controllers
{
    [Route("broadband/[controller]/[action]")]
    public class HomeController: AbpController
    {
        [HttpGet("/broadband")]
        [HttpGet("/broadband/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}