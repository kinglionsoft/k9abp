using K9Abp.Core;
using Microsoft.AspNetCore.Antiforgery;

namespace K9Abp.Web.Host.Controllers
{
    public class AntiForgeryController : K9AbpControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

