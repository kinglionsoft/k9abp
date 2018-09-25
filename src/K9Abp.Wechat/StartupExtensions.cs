using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using K9Abp.Core.Configuration;
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
            services.AddSenparcGlobalServices(configuration) //Senparc.CO2NET 全局注册
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
            register.UseSenparcWeixin(senparcWeixinSetting);
            // TODO: 注册公众号（可注册多个） 需要从数据库中读取各个租户额配置数据，全部注册

            foreach (var mp in GetWeixinMpSettings(app.ApplicationServices))
            {
                register.RegisterMpAccount(mp[0], mp[1], mp[2]);
            }

            //除此以外，仍然可以在程序任意地方注册公众号或小程序：
            //AccessTokenContainer.Register(appId, appSecret, name);//命名空间：Senparc.Weixin.MP.Containers
        }

        private static List<string[]> GetWeixinMpSettings(IServiceProvider serviceProvider)
        {
            var uow = serviceProvider.GetService<IUnitOfWorkManager>();
            var repository = serviceProvider.GetService<IRepository<Setting, long>>();
            string[] names =
            {
                AppSettings.TenantManagement.WechatAppId,
                AppSettings.TenantManagement.WechatAppSecret,
                AppSettings.TenantManagement.WechatAppName
            };
            using (uow.Current.SetTenantId(null))
            {
                return repository.GetAll()
                     .Where(x => names.Contains(x.Name) && x.TenantId != null)
                     .GroupBy(x => x.TenantId)
                     .Select(x => new []
                     {
                        x.First(n => n.Name == AppSettings.TenantManagement.WechatAppId).Value,
                        x.First(n => n.Name == AppSettings.TenantManagement.WechatAppSecret).Value,
                        x.First(n => n.Name == AppSettings.TenantManagement.WechatAppName).Value
                     }).ToList();
            }
        }
    }
}