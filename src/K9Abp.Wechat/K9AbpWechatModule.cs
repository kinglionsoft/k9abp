using System;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using K9Abp.Web.Core.Authentication.External;

namespace K9Abp.Wechat
{
    public class K9AbpWechatModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.Zero().UserManagement.ExternalAuthenticationSources.Add<Wechat.WechatExternalAuthSource>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(K9AbpWechatModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            externalAuthConfiguration.Providers.Add(
                new ExternalLoginProviderInfo("wechat", "id", "secret", typeof(WechatExternalAuthProviderApi)));
        }
    }
}
