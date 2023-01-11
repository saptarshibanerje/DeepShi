using DeepShiApi.TokenRepository;
//using DeepShiEntityContext.Data.IRepository;
//using DeepShiEntityContext.Data.SqlRepository;
using DeepShiEntityContext.DBContext;
using DeepShiEntityModels.Models;
using DeepShiShared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeepShiApi", Version = "v1" });
            });

            SiteKeys.Configure(Configuration.GetSection("Jwt"));
            var key = Encoding.ASCII.GetBytes(SiteKeys.Token);

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(60);
            //});

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                //options.RequireHttpsMetadata = false;
                //options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = SiteKeys.WebSiteDomain,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddEntityFrameworkSqlServer();
            //services.AddDbContextPool<AppDbContext>((serviceProvider, optionsBuilder) =>
            //{
            //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectionString"));
            //    optionsBuilder.UseInternalServiceProvider(serviceProvider);
            //});

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequiredUniqueChars = 3;
                option.Password.RequireUppercase = true;
                option.Password.RequireLowercase = true;
                option.User.RequireUniqueEmail = true;

                option.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //services.AddSession();
            #region Add Scoped

            services.AddScoped<ITokenService, TokenService>();
           
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeepShiApi v1"));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            #region "JWT Token For Authentication Login"    

            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseSession();
            #endregion


            app.UseCors("AllowMyOrigin");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
