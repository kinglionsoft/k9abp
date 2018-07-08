using System.Reflection;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Resources.Embedded;
using K9Abp.Core;

namespace K9AbpPlugin.PomotionQuery
{
    [DependsOn(
        typeof(K9AbpCoreModule))]
    public class PromotionModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(PromotionModule).Assembly,
                    moduleName: "promotion",
                    useConventionalHttpVerbs: true);

            Configuration.EmbeddedResources.Sources.Add(
                new EmbeddedResourceSet(
                    "/Views/",
                    Assembly.GetExecutingAssembly(),
                    "Promotion.Views"
                )
            );
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(PromotionModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
