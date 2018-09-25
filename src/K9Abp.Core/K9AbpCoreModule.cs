using System;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.Net.Mail.Smtp;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Configuration.Startup;
using Abp.MailKit;
using Castle.MicroKernel.Registration;
using Microsoft.AspNetCore.Hosting;
using K9Abp.Core.Authorization.Roles;
using K9Abp.Core.Authorization.Users;
using K9Abp.Core.Chat;
using K9Abp.Core.Configuration;
using K9Abp.Core.Debugging;
using K9Abp.Core.Emailing;
using K9Abp.Core.Features;
using K9Abp.Core.Friendships;
using K9Abp.Core.Friendships.Cache;
using K9Abp.Core.I18N;
using K9Abp.Core.Localization;
using K9Abp.Core.MultiTenancy;
using K9Abp.Core.MultiTenancy.Payments.Cache;
using K9Abp.Core.Notifications;
using K9Abp.Core.Timing;

namespace K9Abp.Core
{
    [DependsOn(typeof(AbpZeroCoreModule),
            typeof(AbpAutoMapperModule),
            typeof(ORS.AspNetZeroCore.Web.AspNetZeroCoreWebModule),
            typeof(AbpMailKitModule)
        )]
    public class K9AbpCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;

        public K9AbpCoreModule(IHostingEnvironment env)
        {
            _env = env;
        }

        public override void PreInitialize()
        {
            // Eable entity history storing
            Configuration.EntityHistory.IsEnabled = true;

            // Allow anonymous uesrs
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);
            
            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = K9AbpConsts.MultiTenancyEnabled;
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = "{0}.k9.com";

            //Adding feature providers
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            //Adding setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            //Adding notification providers
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();

            // Localization
            K9AbpLocalizationConfigurer.Configure(Configuration.Localization, _env.ContentRootPath);

            // i18n
            K9AbpI18NConfigurer.Configure(_env.ContentRootPath);

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            if (DebugHelper.IsDebug)
            {
                //Disabling email sending in debug mode
                Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
            }

            Configuration.ReplaceService(typeof(IEmailSenderConfiguration), () =>
            {
                Configuration.IocManager.IocContainer.Register(
                    Component.For<IEmailSenderConfiguration, ISmtpEmailSenderConfiguration>()
                        .ImplementedBy<K9AbpSmtpEmailSenderConfiguration>()
                        .LifestyleTransient()
                );
            });

            Configuration.Caching.Configure(FriendCacheItem.CacheName, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(30);
            });

            Configuration.Caching.Configure(PaymentCacheItem.CacheName, cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(K9AbpConsts.PaymentCacheDurationInMinutes);
            });

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(K9AbpCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IChatCommunicator, NullChatCommunicator>();

            IocManager.Resolve<ChatUserStateWatcher>().Initialize();
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}

