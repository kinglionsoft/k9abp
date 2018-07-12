using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Modules;
using Abp.Reflection.Extensions;
using K9Abp.Application.Menu;
using K9Abp.Core;
using K9Abp.Core.Authorization;
using K9Abp.EntityFrameworkCore.Repositories;

namespace K9Abp.Application
{
    [DependsOn(
        typeof(K9AbpCoreModule)
        )]
    public class K9AbpApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<K9AbpAuthorizationProvider>();

            Configuration.Navigation.Providers.Add<AppNavigationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(K9AbpApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}

