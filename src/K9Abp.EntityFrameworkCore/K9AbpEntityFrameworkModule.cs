using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using K9Abp.Core;
using K9Abp.EntityFrameworkCore.Repositories;
using K9Abp.EntityFrameworkCore.Seed;

namespace K9Abp.EntityFrameworkCore
{
    [DependsOn(
        typeof(K9AbpCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule),
        typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
        )]
    public class K9AbpEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }


        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<K9AbpDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        K9AbpDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        K9AbpDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(K9AbpEntityFrameworkModule).GetAssembly());
            IocManager.Register(typeof(IRepository<,>), typeof(K9AbpRepositoryBase<,>), DependencyLifeStyle.Transient);
            IocManager.Register(typeof(IRepository<>), typeof(K9AbpRepositoryBase<>), DependencyLifeStyle.Transient);
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}

