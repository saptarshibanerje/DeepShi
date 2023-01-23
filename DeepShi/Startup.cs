using DeepShiEntityModels;
using DeepShiShared;
using DeepShiShared.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace DeepShi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ProtectionKey>();
            services.AddSingleton(Configuration);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMemoryCache();

            services.AddHttpClient<ApiHelper>(options =>
            {
                int timeout = string.IsNullOrWhiteSpace(Configuration.GetAppConfig("ApiRequestTimeout")) ? 1 : int.Parse(Configuration.GetAppConfig("ApiRequestTimeout"));
                options.BaseAddress = new Uri(Configuration.GetAppConfig("ApiUrl"));
                options.Timeout = TimeSpan.FromMinutes(timeout);

            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                int connectionLifeTime = string.IsNullOrWhiteSpace(Configuration.GetAppConfig("ApiConnectionLifeTime")) ? 10 : int.Parse(Configuration.GetAppConfig("ApiConnectionLifeTime")); ;
                int connectionIdleTimeout = string.IsNullOrWhiteSpace(Configuration.GetAppConfig("ApiConnectionIdleTimeout")) ? 5 : int.Parse(Configuration.GetAppConfig("ApiConnectionIdleTimeout")); ;
                int maxConnectionsPerServer = string.IsNullOrWhiteSpace(Configuration.GetAppConfig("ApiMaxConnectionsPerServer")) ? 10 : int.Parse(Configuration.GetAppConfig("ApiMaxConnectionsPerServer")); ;

                return new SocketsHttpHandler()
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(connectionLifeTime),
                    PooledConnectionIdleTimeout = TimeSpan.FromMinutes(connectionIdleTimeout),
                    MaxConnectionsPerServer = maxConnectionsPerServer,

                };
            });

            services.AddJsonLocalization(option =>
            {
                option.ResourcesPath = "Languages";
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes((string.IsNullOrWhiteSpace(Configuration.GetAppConfig("SessionTimeout")) ? 30 : int.Parse(Configuration.GetAppConfig("SessionTimeout"))));
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = ".DeepShiSession";

            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }).AddRazorRuntimeCompilation();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseHsts();
            }

            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-GB")
            };

            RequestLocalizationOptions options = new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture("en-GB"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapAreaControllerRoute(
                    name: "Master",
                    areaName: "Master",
                    pattern: "Master/{controller=Home}/{action=Index}/{id?}/{id1?}/{id2?}/{id3?}/{id4?}/{id5?}/{id6?}");

                endpoints.MapAreaControllerRoute(
                    name: "User",
                    areaName: "User",
                    pattern: "User/{controller=Home}/{action=Index}/{id?}/{id1?}/{id2?}/{id3?}/{id4?}/{id5?}/{id6?}");

                endpoints.MapAreaControllerRoute(
                    name: "Transaction",
                    areaName: "Transaction",
                    pattern: "Transaction/{controller=Home}/{action=Index}/{id?}/{id1?}/{id2?}/{id3?}/{id4?}/{id5?}/{id6?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ComingSoon}/{action=index}/{id?}");
            });
        }
    }
}
