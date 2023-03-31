using DeepShiEntityContext.Helper;
using DeepShiEntityModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeepShiEntityContext.DBContext
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public static readonly string ConnectionString = "server=N1NWPLSK12SQL-v03.shr.prod.ams1.secureserver.net:1433;database=DeepShiDB;User Id=DeepShiAdmin;Password=95K0aeu%;";
        //public AppDbContext()
        //{
        //    //if (!optionsBuilder.IsConfigured)
        //    //    optionsBuilder.UseSqlServer("server=DeepShi-SRV-WAPSRV;database=DeepShi;User Id=SqlAdmin;Password=Server@3456;");
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer("server=DeepShi-SRV-WAPSRV;database=DeepShi1;User Id=SqlAdmin;Password=Server@3456;");
            //return new AppDbContext(optionsBuilder.Options);

        }

        //public DbSet<Client> Clients { get; set; }
        //public DbSet<Post> Posts { get; set; }
        //public DbSet<Grade> Grades { get; set; }
        //public DbSet<Project> Projects { get; set; }
        //public DbSet<AdditionalserviceForProject> AdditionalserviceForProjects { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }



    }
}
