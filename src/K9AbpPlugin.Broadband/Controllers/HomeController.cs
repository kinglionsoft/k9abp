using Microsoft.AspNetCore.Mvc;

namespace K9AbpPlugin.Broadband.Controllers
{
    public class HomeController: BroadbandControllerBase
    {
        [HttpGet("/broadband")]
        [HttpGet("/broadband/home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}