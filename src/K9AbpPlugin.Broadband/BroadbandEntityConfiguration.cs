using K9Abp.Core;
using K9AbpPlugin.Broadband.User;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.Broadband
{
    internal class BroadbandEntityConfiguration: IEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<BroadbandUser>(b =>
            {
                b.ToTable("PluginBroadbandUser");
                b.Property(e => e.Extra).HasColumnType("json");
                b.HasIndex(x => x.Phone).HasName("idx_phone");
            });
        }
    }
}