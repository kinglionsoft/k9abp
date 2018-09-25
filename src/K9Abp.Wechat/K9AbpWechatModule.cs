using System;
using Abp.Modules;
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

        public override void PostInitialize()
        {
            ConfigureExternalAuthProviders();
        }

        private void ConfigureExternalAuthProviders()
        {
            var externalAuthConfiguration = IocManager.Resolve<ExternalAuthConfiguration>();

            //not implemented yet. 
            externalAuthConfiguration.Providers.Add(
                new ExternalLoginProviderInfo("wechat", "id", "secret", typeof(WechatExternalAuthProviderApi)));
        }
    }
}
