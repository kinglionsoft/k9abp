using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP;
using Senparc.Weixin.RegisterServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StartupExtensions
    {

        public static void AddSenpacService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSenparcGlobalServices(configuration)//Senparc.CO2NET 全局注册
                .AddSenparcWeixinServices(configuration);//Senparc.Weixin 注册
        }

        public static void UserSenpacService(this IApplicationBuilder app,
            IHostingEnvironment env,
            IConfiguration configuration)
        {

            var senparcSetting = app.ApplicationServices.GetService<IOptions<SenparcSetting>>().Value;
            var senparcWeixinSetting = app.ApplicationServices.GetService<IOptions<SenparcWeixinSetting>>().Value;

            // 启动 CO2NET 全局注册，必须！
            IRegisterService register = RegisterService.Start(env, senparcSetting)
                //关于 UseSenparcGlobal() 的更多用法见 CO2NET Demo：https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore/Startup.cs
                .UseSenparcGlobal();

            //如果需要自动扫描自定义扩展缓存，可以这样使用：
            //register.UseSenparcGlobal(true);
            //如果需要指定自定义扩展缓存，可以这样用：
            //register.UseSenparcGlobal(false, GetExCacheStrategies);

            //开始注册微信信息，必须！
            register.UseSenparcWeixin(senparcWeixinSetting, senparcSetting)
                //注册公众号（可注册多个）
                .RegisterMpAccount(senparcWeixinSetting.WeixinAppId,
                    senparcWeixinSetting.WeixinAppSecret,
                    configuration["SenparcWeixinSetting:MpName"]);

            //除此以外，仍然可以在程序任意地方注册公众号或小程序：
            //AccessTokenContainer.Register(appId, appSecret, name);//命名空间：Senparc.Weixin.MP.Containers
        }
    }
}