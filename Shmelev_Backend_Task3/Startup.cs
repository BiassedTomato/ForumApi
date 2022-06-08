using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shmelev_Backend_Task3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shmelev_Backend_Task3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;


        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ForumContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Forum")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IThreadService, ThreadService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAdministrationService, AdministrationService>();

            /*Mapper profile*/
            services.AddAutoMapper(typeof(ForumMapperProfile));

            services.AddDefaultIdentity<ForumUser>(options =>
            options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<ForumRole>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ForumContext>()
                .AddDefaultTokenProviders();

            services.AddRazorPages();

            //InitRoles(services);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<ForumContext>();
            //    context.Database.EnsureCreated();
            //}

            //Task.Run(() => CreateAdmin(app));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
        }
    }
}
