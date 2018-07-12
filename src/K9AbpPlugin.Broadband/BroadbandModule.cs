using System.Reflection;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Resources.Embedded;
using Abp.Threading.BackgroundWorkers;
using K9Abp.Core;
using K9AbpPlugin.Broadband.Jobs;

namespace K9AbpPlugin.Broadband
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

            Configuration.EmbeddedResources.Sources.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    Assembly.GetExecutingAssembly(),
                    "K9AbpPlugin.Broadband.Views"
                )
            );

            Configuration.Settings.Providers.Add<BroadbandSettingProvider>();
        }

        public override void PostInitialize()
        {
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BroadbandModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
