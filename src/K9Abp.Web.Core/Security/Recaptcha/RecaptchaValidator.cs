using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Extensions;
using Abp.UI;
using K9Abp.Application.Security.Recaptcha;
using K9Abp.Core;
using Microsoft.AspNetCore.Http;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Shapes;
using Path = SixLabors.Shapes.Path;
using PointF = SixLabors.Primitives.Point;

namespace K9Abp.Web.Core.Security.Recaptcha
{
    public class RecaptchaValidator : K9AbpServiceBase, IRecaptchaValidator, ISingletonDependency
    {
        private const string SessionKey = "recaptcha";

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Font _defaultFont;

        public RecaptchaValidator(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _defaultFont = GetFont();
        }

        public void Validate(string captchaResponse)
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
            var code = httpContext.Session.GetString(SessionKey);
            if (code == null || code != captchaResponse)
            {
                throw new UserFriendlyException(L("IncorrectCaptchaAnswer"));
            }
        }

        private Font GetFont()
        {
            var fontFamilies = SystemFonts.Families.ToArray();
            var fontF = fontFamilies.FirstOrDefault(x => x.Name == "Arial");
            if (fontF == null)
            {
                fontF = fontFamilies.First();
            }
            return new Font(fontF, 18);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GetCaptcha(int length = 4)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new Exception("RecaptchaValidator should be used in a valid HTTP context!");
            }

            var random = new Random();
            var captcha = random.Next((int)Math.Pow(10, length), (int)Math.Pow(10, length + 1))
                .ToString()
                .Substring(1);

            httpContext.Session.SetString(SessionKey, captcha);

            //颜色列表，用于验证码、噪线、噪点 
            var colors = new[] { Rgba32.Black, Rgba32.Red, Rgba32.DarkBlue, Rgba32.Green, Rgba32.Orange, Rgba32.Brown, Rgba32.DarkCyan, Rgba32.Purple };
            //创建画布
            var imageWidth = captcha.Length * 13;
            using (var image = new Image<Rgba32>(imageWidth, 28))
            {
                //背景设为白色  
                image.Mutate(x => x.BackgroundColor(Rgba32.White));

                //向画板中绘制贝塞尔样条  
                for (var i = 0; i < 2; i++)
                {
                    var p1 = new PointF(0, random.Next(image.Height));
                    var p2 = new PointF(random.Next(image.Width), random.Next(image.Height));
                    var p3 = new PointF(random.Next(image.Width), random.Next(image.Height));
                    var p4 = new PointF(image.Width, random.Next(image.Height));
                    var clr = colors[random.Next(colors.Length)];
                    image.Mutate(x => x.DrawBeziers(
                                            GraphicsOptions.Default, 
                                            new SolidBrush<Rgba32>(clr),
                                            1f, 
                                            p1, p2, p3, p4));
                }
                //画噪点
                for (var i = 0; i < 50; i++)
                {
                    IBrush<Rgba32> brush = new SolidBrush<Rgba32>(Rgba32.LightGray);
                    Path path = new RegularPolygon(new PointF(random.Next(image.Width), random.Next(image.Height)), 3, 1);
                    image.Mutate(x => x.Draw(GraphicsOptions.Default, brush, 1f, path));
                }
                //画验证码字符串 
                image.Mutate(x =>
                {
                    for (var i = 0; i < captcha.Length; i++)
                    {
                        var cindex = random.Next(colors.Length - 1);//随机颜色索引值  
                        var brush = new SolidBrush<Rgba32>(colors[cindex]);//颜色  
                        var ii = random.Next(4, 8);// 控制验证码不在同一高度  
                      
                        x.DrawText(captcha[i].ToString(), _defaultFont, brush, new PointF(3 + (i * 12), ii));
                    }
                });
                using (var stream = new MemoryStream())
                {
                    image.Save(stream, new JpegEncoder());
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
        }
    }
}

