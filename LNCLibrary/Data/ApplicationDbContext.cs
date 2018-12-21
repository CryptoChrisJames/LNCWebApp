using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LNCWebApp.Models;
using LNCLibrary.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LNCWebApp.Data
{

    public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //IConfigurationRoot configuration = new ConfigurationBuilder()
                //   .SetBasePath(Directory.GetCurrentDirectory())
                //   .AddJsonFile("appsettings.json")
                //   .Build();
                //var connectionString = configuration.GetConnectionString("DefaultConnection");
                var connectionString = "Data Source=obsidiandb.crjvbstix97q.us-east-2.rds.amazonaws.com,1433;Initial Catalog=LNCTest;" +
                    "User ID=obsidiantech;Password=Obsidian12!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
                    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                //var connectionString = "Data Source=CHRISTOPHER09E8;Initial Catalog=LNCTest;Integrated Security=True;" +
                //    "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
