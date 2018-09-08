using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using LNCWebApp.Data;
using LNCWebApp.Models;
using LNCWebApp.Services;
using Microsoft.Extensions.Caching;
using LNCLibrary.Models;
using Microsoft.AspNetCore.Http;
using Stripe;
using LNCLibrary.Configurations.StripeConfig;
using Microsoft.AspNetCore.Identity;

namespace LNCWebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.CookieName = ".LNCWebApp";
            });
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider sp)
        {
            app.UseSession();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(sp).Wait();
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);
            
        }

        public async Task CreateRoles(IServiceProvider sp)
        {
            //add roles 
            var RoleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            string role = "Admin";
            IdentityResult roleresult;

            //Create role if it doesn't already exist
            var roleexist = await RoleManager.RoleExistsAsync(role);
            if(!roleexist)
            {
                roleresult = await RoleManager.CreateAsync(new IdentityRole(role));
            }

            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("Admin1")["Email"],
                Email = Configuration.GetSection("Admin1")["Email"]
            };

            string UserPassword = Configuration.GetSection("Admin1")["Password"];
            var user = await UserManager
                .FindByEmailAsync(Configuration
                .GetSection("Admin1")["Email"]);
            if(user == null)
            {
                var createpoweruser = await UserManager.CreateAsync(poweruser, UserPassword);
                if(createpoweruser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
            else
            {
                await UserManager.AddToRoleAsync(poweruser, "Admin");
            }
        }
        
    }
}
