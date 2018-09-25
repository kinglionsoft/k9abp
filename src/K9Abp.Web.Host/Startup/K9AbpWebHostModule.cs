using ORS.AspNetZeroCore;
using Abp.Configuration.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using K9Abp.Core.Configuration;
using K9Abp.Core.MultiTenancy;
using K9Abp.EntityFrameworkCore;
using K9Abp.Web.Core;
using K9Abp.Web.Core.Authentication.External;
using K9Abp.Wechat;
using Abp.Zero.Configuration;

namespace K9Abp.Web.Host.Startup
{
    [DependsOn(
       typeof(K9AbpWebCoreModule),
        typeof(K9AbpWechatModule))]
    
    public class K9AbpWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public K9AbpWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.Zero().UserManagement.ExternalAuthenticationSources.Add<Wechat.WechatExternalAuthSource>();
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:MultiTenancy.DomainFormat"];
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(K9AbpWebHostModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                return;
            }

            if (!DatabaseCheckHelper.Exist(_appConfiguration["ConnectionStrings:Default"]))
            {
                return;
            }

            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<SubscriptionExpirationCheckWorker>());
            workManager.Add(IocManager.Resolve<SubscriptionExpireEmailNotifierWorker>());

            ConfigureExternalAuthProviders();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            //not implemented yet. 
            externalAuthConfiguration.Providers.Add(
                new ExternalLoginProviderInfo("wechat","id","secret", typeof(WechatExternalAuthProviderApi)));
        }
    }
}

