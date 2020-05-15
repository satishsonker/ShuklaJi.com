using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShuklaJi.ModelLayer.Areas.Admin.UserManagement;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Shuklaji.com.Models;
using ShuklaJi.BusinessLayer.Areas.Admin.AppSetting;
using ShuklaJi.ModelLayer.Areas.Admin.AppSetting;

namespace Shuklaji.com
{
    public class Startup
    {
        static DowntimeModel downtimeModel = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DowntimeDetails downtimeDetails = new DowntimeDetails();
            downtimeModel = downtimeDetails.HasAppDowntime();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddMvc().AddRazorPagesOptions(options => {
            //    //options.Conventions.AddPageRoute("/Dashboard/Index", "");
            //}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuthentication().AddGoogle(option => {
                option.ClientId = "835937395382-eqbup1k0tra01c6e8piqhtli9cm9o8rj.apps.googleusercontent.com";
                option.ClientSecret = "DwKVOoLmtu1KWHdNcvOUZB7b";
            });
            services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("Dev")));

            services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddMvc().AddRazorPagesOptions(options => {
                options.Conventions.AddPageRoute("/Dashboard", "/");
                options.Conventions.AddPageRoute("/Dashboard", "home");
                options.Conventions.AddPageRoute("/Dashboard", "index");
                options.Conventions.AddPageRoute("/Dashboard/index", "");
                //options.Conventions.AddAreaPageRoute("admin","/Dashboard/Index", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.Use(async (context, next) =>
            {
                await next.Invoke();
               
                
                if (downtimeModel!=null && downtimeModel.FromTime!=DateTime.MinValue && !context.Request.Path.ToUriComponent().Contains("/admin/") && !context.Request.Path.ToUriComponent().Contains(".css") && !context.Request.Path.ToUriComponent().Contains(".js"))
                {
                    context.Response.Redirect("/common/ErrorHandler/downtime");
                }
                else if(context.Request.Path.ToUriComponent()=="/")
                {
                    context.Response.Redirect("/web/home/index");
                }
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areas", 
                //    template: "{area:exists}/{controller}/{action}/{id?}"
                //  );


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=index}/{id?}");

            });
        }
    }
}
