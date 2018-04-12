using System.Threading.Tasks;
using Abp.Net.Mail;
using K9Abp.Application.Configuration.Host.Dto;

namespace K9Abp.Application.Configuration
{
    public abstract class SettingsAppServiceBase : K9AbpAppServiceBase
    {
        private readonly IEmailSender _emailSender;

        protected SettingsAppServiceBase(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        #region Send Test Email

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            await _emailSender.SendAsync(
                input.EmailAddress,
                L("TestEmail_Subject"),
                L("TestEmail_Body")
            );
        }

        #endregion
    }
}

