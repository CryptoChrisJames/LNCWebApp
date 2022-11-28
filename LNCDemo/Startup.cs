using LNCDemo.Services;
using LNCLibrary.Configurations.StripeConfig;
using LNCLibrary.Data;
using LNCLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Threading.Tasks;

namespace LNCDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
<<<<<<< HEAD
            // Add framework services.
=======
            var currentConnString = currentEnv == "Development" ? "DefaultConnection" : "Server=sql;Database=LNCDemo;User=sa;Password=DEMOS123;";
>>>>>>> 33d855d32b259f4b40c024414b58797ad050fcd9
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Env.IsDevelopment() ?
                    Configuration.GetConnectionString("DefaultConnection") :
                    "Server=sql;Database=LNCDemo;User=sa;Password=Demos123!;"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp)
        {
            if (!Env.IsDevelopment())
            {
                using (IServiceScope scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    scope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
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
            if (!roleexist)
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
            if (user == null)
            {
                var createpoweruser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createpoweruser.Succeeded)
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
