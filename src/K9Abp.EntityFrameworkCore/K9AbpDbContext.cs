using System;
using System.IO;
using System.Linq;
using System.Resources;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityServer4;
using Abp.Organizations;
using Abp.PlugIns;
using Abp.Zero.EntityFrameworkCore;
using K9Abp.Core;
using Microsoft.EntityFrameworkCore;
using K9Abp.Core.Authorization.Roles;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Chat;
using K9Abp.Core.Editions;
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

        public virtual DbSet<Distinct> Distincts { get; set; }

        public virtual DbSet<County> Counties { get; set; }

        public virtual DbSet<DistinctOrganizationUnit> DistinctOrganizationUnits { get; set; }

        // TODO: Define an IDbSet for each entity of the application 

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

            modelBuilder.Entity<DistinctOrganizationUnit>(b =>
            {
                b.HasIndex(e => e.DistinctId);
            });

            modelBuilder.ConfigurePersistedGrantEntity();

            ApplyEntitiesFromPlugins(modelBuilder);

            // TODO: configure the model here

            // Naming convention
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.Relational().TableName;
                modelBuilder.Entity(entity.Name).ToTable(currentTableName.ToSingular().ToSnakeCase());
            }
        }


        /// <summary>
        /// Configure Entities in Plugin Modules
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected virtual void ApplyEntitiesFromPlugins(ModelBuilder modelBuilder)
        {
            IAbpPlugInManager abpPlugInManager;
            if (IocManager.Instance.IsRegistered<IAbpPlugInManager>())
            {
                abpPlugInManager = IocManager.Instance.Resolve<IAbpPlugInManager>();
            }
            else // run by `dotnet ef`
            {
                var pluginRoot = WebContentDirectoryFinder.FindPluginsFolder();
                if (!Directory.Exists(pluginRoot))
                {
                    return;
                }
                abpPlugInManager = new AbpPlugInManager();
                abpPlugInManager.PlugInSources.AddFolder(pluginRoot);
            }
            var configurationTypes = abpPlugInManager.PlugInSources.GetAllAssemblies()
                .SelectMany(x => x.DefinedTypes
                                .Where(p => !p.IsAbstract && p.IsClass && p.GetInterface(nameof(IEntityConfiguration)) != null)
                                .Select(t => t.AsType()));
            foreach (var configuration in configurationTypes)
            {
                try
                {
                    (Activator.CreateInstance(configuration) as IEntityConfiguration).Configure(modelBuilder);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Can't configure entities of {configuration.FullName}", ex);
                }
            }
        }
    }
}

