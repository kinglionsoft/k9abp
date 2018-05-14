using System;
using System.Data;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace K9Abp.Web.Host.Startup
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddAbpHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
            {
               // config.UseNLogLogProvider(); // DO NOT enable this configuration.
            });
            GlobalConfiguration.Configuration.UseStorage(
                new MySqlStorage(
                    connectionString,
                    new MySqlStorageOptions
                    {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 50000,
                        TransactionTimeout = TimeSpan.FromMinutes(1),
                    }));
            return services;
        }

        public static IApplicationBuilder UserAbpHangfire(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            int.TryParse(configuration["WorkerCount"], out var workerCount);
            app.UseHangfireServer(
                new BackgroundJobServerOptions
                {
                    WorkerCount = workerCount > 0 ? workerCount : 1,
                    Activator = new ContainerJobActivator(app.ApplicationServices)
                });
            return app;
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.Identity.IsAuthenticated || context.Request.LocalIpAddress == "127.0.0.1" || context.Request.LocalIpAddress == "::1";
        }
    }
    public class ContainerJobActivator : JobActivator
    {
        private readonly IServiceProvider _container;

        public ContainerJobActivator(IServiceProvider container)
        {
            _container = container;
        }

        public override object ActivateJob(Type type)
        {
            return _container.GetService(type);
        }
    }
}