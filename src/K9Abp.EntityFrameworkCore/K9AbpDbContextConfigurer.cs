using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.EntityFrameworkCore
{
    public static class K9AbpDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<K9AbpDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<K9AbpDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}

