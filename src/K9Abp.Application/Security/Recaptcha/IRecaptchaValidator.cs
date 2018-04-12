using System.Threading.Tasks;

namespace K9Abp.Application.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}
