using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using K9Abp.Core;

namespace ProductDemoPlugin
{
    [DependsOn(
        typeof(K9AbpCoreModule))]
    public class ProductDemoModule: AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(ProductDemoModule).Assembly, 
                    moduleName: "ProductDemo", 
                    useConventionalHttpVerbs: true);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ProductDemoModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}



