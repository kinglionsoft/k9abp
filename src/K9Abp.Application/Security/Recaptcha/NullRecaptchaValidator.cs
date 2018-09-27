using System.Threading.Tasks;

namespace K9Abp.Application.Security.Recaptcha
{
    public class NullRecaptchaValidator : IRecaptchaValidator
    {
        public static NullRecaptchaValidator Instance { get; } = new NullRecaptchaValidator();

        public void Validate(string captchaResponse)
        {
            
        }

        public byte[] GetCaptcha(int length = 4)
        {
            return new byte[0];
        }
    }
}
