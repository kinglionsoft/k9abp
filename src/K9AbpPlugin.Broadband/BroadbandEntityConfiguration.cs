using K9Abp.Core;
using K9AbpPlugin.Broadband.User;
using K9AbpPlugin.Broadband.Warn;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.Broadband
{
    internal class BroadbandEntityConfiguration: IEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<BroadbandUser>(b =>
            {
                b.Property(e => e.Extra).HasColumnType("json");
                b.HasIndex(x => x.Phone).HasName("idx_phone");
                b.HasIndex(x => x.TenantId).HasName("idx_tanent");
                b.HasIndex(x => x.OrganizationUnitId).HasName("idx_ou");
            });

            builder.Entity<BroadbandWarn>(b =>
            {
                b.HasIndex(x => x.CountyId).HasName("idx_county");
                b.HasIndex(x => x.DistinctId).HasName("idx_distinct");
                b.HasIndex(x => x.OrganizationUnitId).HasName("idx_channel");
                b.HasIndex(x => x.TenantId).HasName("idx_tanent");
            });
        }
    }
}