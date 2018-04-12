using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using PaulMiami.AspNetCore.Mvc.Recaptcha;
using K9Abp.Application.Security.Recaptcha;
using K9Abp.Core;

namespace K9Abp.Web.Core.Security.Recaptcha
{
    public class RecaptchaValidator : K9AbpServiceBase, IRecaptchaValidator, ITransientDependency
    {
        public const string RecaptchaResponseKey = "g-recaptcha-response";

        private readonly IRecaptchaValidationService _recaptchaValidationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RecaptchaValidator(IRecaptchaValidationService recaptchaValidationService, IHttpContextAccessor httpContextAccessor)
        {
            _recaptchaValidationService = recaptchaValidationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task ValidateAsync(string captchaResponse)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("RecaptchaValidator should be used in a valid HTTP context!");
            }

            if (captchaResponse.IsNullOrEmpty())
            {
                throw new UserFriendlyException(L("CaptchaCanNotBeEmpty"));
            }

            try
            {
                await _recaptchaValidationService.ValidateResponseAsync(
                    captchaResponse,
                    _httpContextAccessor.HttpContext.Connection?.RemoteIpAddress?.ToString()
                );
            }
            catch (RecaptchaValidationException)
            {
                throw new UserFriendlyException(L("IncorrectCaptchaAnswer"));
            }
        }
    }
}

