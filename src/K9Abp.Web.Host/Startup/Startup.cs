using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Abp.AspNetCore;
using Abp.Castle.NLogLogging;
using Abp.Extensions;
using Abp.PlugIns;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Hangfire;
using K9Abp.Core.Configuration;
using K9Abp.Core.Identity;
using K9Abp.Core.Web;
using K9Abp.Web.Core.Authentication.JwtBearer;
#if DEBUG
using Swashbuckle.AspNetCore.Swagger;
#endif

#if FEATURE_SIGNALR
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using Abp.Owin;
using K9Abp.Web.Core.Owin;
#endif

namespace K9Abp.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment env)
        {
            _environment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName));
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            // Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(_defaultCorsPolicyName, builder =>
                {
                    // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                    builder
                        .WithOrigins(_appConfiguration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray())
                        .SetPreflightMaxAge(TimeSpan.FromDays(1))
                        .AllowAnyHeader()
                        .WithMethods("OPTIONS", "GET", "POST", "PUT", "DELETE");
                });
            });

            // hangfire
            services.AddAbpHangfire(_appConfiguration.GetConnectionString("Default"));

#if DEBUG
            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "K9Abp API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                // Assign scope requirements to operations based on AuthorizeAttribute
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                
                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var xmlForApi = Path.Combine(basePath, "K9Abp.Web.Core.xml");
                options.IncludeXmlComments(xmlForApi);
            });
#endif

            // Configure Abp and Dependency Injection
            return services.AddAbp<K9AbpWebHostModule>(options =>
            {   
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpNLog().WithConfig(Path.Combine("config", $"nlog.{_environment.EnvironmentName}.config")));
                var pluginsPath = WebContentDirectoryFinder.FindPluginsFolder();
                if (Directory.Exists(pluginsPath))
                {
                    options.PlugInSources.AddFolder(pluginsPath);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options =>
            {
                options.UseAbpRequestLocalization = false; //disable automatic adding of request localization
            }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseEmbeddedFiles();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseAbpRequestLocalization(); // after authentication middleware

            app.UserAbpHangfire(_appConfiguration.GetSection("Hangfire")); // after authentication middleware

#if FEATURE_SIGNALR
            // Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

#if DEBUG
            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "K9Abp API V1");
                options.DocumentTitle="K9Abp Api";
                options.IndexStream = () => File.OpenRead(Path.Combine(_environment.ContentRootPath, "wwwroot", "swagger", "ui", "index.html"));
            }); // URL: /swagger
#endif
        }

#if FEATURE_SIGNALR
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            app.Properties["host.AppName"] = "K9Abp";

            app.UseAbp();
            
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
#endif
    }
}

