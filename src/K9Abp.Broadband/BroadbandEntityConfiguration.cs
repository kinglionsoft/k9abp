using K9Abp.Broadband.User;
using K9Abp.Core;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.Broadband
{
    internal class BroadbandEntityConfiguration: IEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<BroadbrandUser>(b =>
            {
                b.Property(e => e.Extra).HasColumnType("json");
            });
            builder.Entity<BroadbrandUser>()
                .HasIndex(x => x.Phone).HasName("idx_phone");
        }
    }
}