using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using K9Abp.Core;
using K9Abp.Core.Configuration;
using K9Abp.Core.Web;

namespace K9Abp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class K9AbpDbContextFactory : IDesignTimeDbContextFactory<K9AbpDbContext>
    {
        public K9AbpDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<K9AbpDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.FindConfigurationFolder());

            K9AbpDbContextConfigurer.Configure(builder, configuration.GetConnectionString(K9AbpConsts.ConnectionStringName));

            return new K9AbpDbContext(builder.Options);
        }
    }
}

