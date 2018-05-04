using System;
using System.Data;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace K9Abp.Web.Host.Startup
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddAbpHangfire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(config =>
            {
                config.UseNLogLogProvider();
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

        public static IApplicationBuilder UserAbpHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            app.UseHangfireServer(
                new BackgroundJobServerOptions
                {
                    WorkerCount = 1
                });
            return app;
        }
    }

    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity.IsAuthenticated || context.Request.LocalIpAddress=="127.0.0.1" || context.Request.LocalIpAddress=="localhost";
        }
    }
}