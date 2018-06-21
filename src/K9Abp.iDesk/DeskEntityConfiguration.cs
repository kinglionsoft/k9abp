using K9Abp.Core;
using K9Abp.iDesk.Domain;
using K9Abp.iDesk.Domain.Customer;
using K9Abp.iDesk.Domain.Follower;
using K9Abp.iDesk.Domain.Step;
using K9Abp.iDesk.Domain.Tag;
using Microsoft.EntityFrameworkCore;

namespace K9Abp.iDesk
{
    internal class DeskEntityConfiguration: IEntityConfiguration
    {
        public void Configure(ModelBuilder builder)
        {
            builder.Entity<DeskworkTag>();

            builder.Entity<DeskworkCustomer>(b =>
            {
                b.HasIndex(x => x.Phone).IsUnique();
                b.HasIndex(x => x.Name);
            });

            builder.Entity<DeskworkFollower>(b =>
            {
                b.HasIndex(x => x.WorkId);
                b.HasIndex(x => x.FollowerId);
            });

            builder.Entity<DeskworkStep>(b => { b.HasIndex(x => x.WorkId); });

            builder.Entity<Deskwork>(b => { b.HasIndex(x => x.CustomerId); });
        }
    }
}