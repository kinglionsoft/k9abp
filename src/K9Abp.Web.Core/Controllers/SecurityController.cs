using K9Abp.Application.Security.Recaptcha;
using K9Abp.Core;
using Microsoft.AspNetCore.Mvc;

namespace K9Abp.Web.Core.Controllers
{
    public class SecurityController : K9AbpControllerBase
    {
        public IRecaptchaValidator RecaptchaValidator { get; set; }

        public SecurityController()
        {
            RecaptchaValidator = NullRecaptchaValidator.Instance;
        }

        
        public FileContentResult Captcha()
        {
            return new FileContentResult(RecaptchaValidator.GetCaptcha(), "image/png");
        }
    }
}