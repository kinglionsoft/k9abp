using Abp.Web.Models;
using Abp.Web.Mvc.Models;
using K9Abp.Web.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace K9Abp.Web.Host.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController: K9AbpControllerBase
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("/error/{status}")]
        public IActionResult Index(int status)
        {
            string message;
            var detail = string.Empty;
            switch (status)
            {
                case 404:
                    message = "您访问的页面不存在或者已经删除";
                    break;
                default:
                    message = "服务器发生错误，请稍后再试";
                    break;
            }
            var model = new ErrorViewModel(new ErrorInfo(status, message, detail));
            return View(model);
        }
    }
}