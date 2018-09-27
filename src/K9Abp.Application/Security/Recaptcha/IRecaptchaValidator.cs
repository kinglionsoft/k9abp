using System.Threading.Tasks;

namespace K9Abp.Application.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <param name="captchaResponse"></param>
        /// <returns></returns>
        void Validate(string captchaResponse);

        /// <summary>
        /// 获取验证码图片字节流
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] GetCaptcha(int length = 4);
    }
}
