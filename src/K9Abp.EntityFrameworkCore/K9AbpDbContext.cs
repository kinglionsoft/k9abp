using System.IO;
using System.Linq;
using System.Reflection;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using K9Abp.Core.Authorization.Roles;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Chat;
using K9Abp.Core.Editions;
using K9Abp.Core.EntityDemo;
using K9Abp.Core.Friendships;
using K9Abp.Core.MultiTenancy;
using K9Abp.Core.MultiTenancy.Accounting;
using K9Abp.Core.MultiTenancy.Payments;
using K9Abp.Core.Storage;
using K9Abp.Core.Web;
using K9Abp.EntityFrameworkCore.Repositories;

namespace K9Abp.EntityFrameworkCore
{
    [AutoRepositoryTypes(
        typeof(IRepository<>),
        typeof(IRepository<,>),
        typeof(K9AbpRepositoryBase<>),
        typeof(K9AbpRepositoryBase<,>)
    )]
    public class K9AbpDbContext : AbpZeroDbContext<Tenant, Role, User, K9AbpDbContext>, IAbpPersistedGrantDbContext
    {
        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        // TODO: Define an IDbSet for each entity of the application 

        #region iDesk

        public virtual DbSet<iDeskCore.Work.Customer.DeskworkCustomer> DeskworkCustomers { get; set; }
        public virtual DbSet<iDeskCore.Work.Follower.DeskworkFollower> DeskworkFollowers { get; set; }
        public virtual DbSet<iDeskCore.Work.Step.DeskworkStep> DeskworkSteps { get; set; }
        public virtual DbSet<iDeskCore.Work.DeskworkTag> DeskworkTags { get; set; }
        public virtual DbSet<iDeskCore.Work.Deskwork> Deskworks { get; set; }

        #endregion

        public K9AbpDbContext(DbContextOptions<K9AbpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.ConfigurePersistedGrantEntity();

            // ApplyEntitiesFromPlugins(modelBuilder);

            // Naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.Relational().TableName;
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToSingular().ToSnakeCase());
            }

            // TODO: configure the model here
        }

        // 见：http://10.0.200.18/abp/ykabp/issues/1
        protected virtual void ApplyEntitiesFromPlugins(ModelBuilder modelBuilder)
        {
            var pluginsRoot = Path.Combine(WebContentDirectoryFinder.CalculateContentRootFolder(), "Plugins");
            if(!Directory.Exists(pluginsRoot)) return;
            
            // get all Assemblies
            var dllList = Directory.GetFiles(pluginsRoot, "*.dll", SearchOption.AllDirectories);
            var types = dllList.Select(Assembly.LoadFile)
                .SelectMany(x => x.DefinedTypes.Select(t => t.AsType()));
        }
    }
}

