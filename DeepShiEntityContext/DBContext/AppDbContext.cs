using DeepShiEntityContext.Helper;
using DeepShiEntityModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShiEntityContext.DBContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        //public static readonly string ConnectionString = "server=sql.bsite.net\\MSSQL2016;database=saptarshi_deepshi;User Id=DeepShiAdmin;Password=September@2023#;";
        //public static readonly string ConnectionString = "server=DEEPSHI;database=saptarshi_deepshi;User Id=sa;Password=123456;";

        //public AppDbContext()
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer("server=DeepShi-SRV-WAPSRV;database=DeepShi;User Id=SqlAdmin;Password=Server@3456;");
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer(ConnectionString);            

        }

        //public DbSet<Client> Clients { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Grade> Grades { get; set; }
        //public DbSet<Project> Projects { get; set; }
        //public DbSet<AdditionalserviceForProject> AdditionalserviceForProjects { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer(ConnectionString);

        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            //foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            //}
        }



    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../DeepShiApi/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = configuration.GetConnectionString("ConnectionString");
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
