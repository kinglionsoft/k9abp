using System;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using K9Abp.Core;

namespace K9Abp.iDesk
{
    [DependsOn(
        typeof(K9AbpCoreModule))]
    public class DeskModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(DeskModule).Assembly,
                    moduleName: "Desk",
                    useConventionalHttpVerbs: true);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(DeskModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
