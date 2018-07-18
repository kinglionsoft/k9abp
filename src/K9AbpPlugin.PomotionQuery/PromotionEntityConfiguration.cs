using K9Abp.Core;
using K9AbpPlugin.PomotionQuery.Domain;
using Microsoft.EntityFrameworkCore;

namespace K9AbpPlugin.PomotionQuery
{
    internal class PromotionEntityConfiguration: IEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<Promotion>(b => { b.ToTable("PluginPromotion"); });

            builder.Entity<PromotionTarget>(b =>
            {
                b.HasIndex(x => new { x.Phone, x.TenantId, x.PromotionId});
            });

            builder.Entity<QueryLog>(b =>
            {
                b.HasIndex(x => new {x.TenantId, x.OrganizationUnitId});
            });
        }
    }
}