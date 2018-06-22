using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using K9Abp.Core;

namespace K9Abp.Broadband
{
    [DependsOn(
        typeof(K9AbpCoreModule))]
    public class BroadbandModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(BroadbandModule).Assembly,
                    moduleName: "broadband",
                    useConventionalHttpVerbs: true);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BroadbandModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
