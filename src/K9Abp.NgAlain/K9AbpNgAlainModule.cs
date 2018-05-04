using System;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Web.Configuration;
using K9Abp.NgAlain.Configuration;

namespace K9Abp.NgAlain
{
    public class K9AbpNgAlainModule : AbpModule
    {
        public override void Initialize()
        {
            Configuration.ReplaceService(typeof(AbpUserConfigurationBuilder), typeof(K9AbpUserConfigurationBuilder), DependencyLifeStyle.Transient);
        }
    }
}
