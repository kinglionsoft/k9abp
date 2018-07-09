using System.Reflection;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Resources.Embedded;
using K9Abp.Core;

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
                    "K9Abp.Broadband.Views"
                )
            );
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BroadbandModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
