using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCTest.Services;

namespace MVCTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            string connection = Configuration.GetConnectionString("DefaultConnection");                 //DB connection
            services.AddDbContext<Models.SiteDbContext>(options => options.UseSqlServer(connection));   //



            services.AddDistributedMemoryCache();//Sessions
            services.AddSession();               //

            services.AddTransient<IFilterService, FilterService>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}"
                    );


                routes.MapRoute(
                    name: "buy",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );

                //routes.MapRoute(
                //    name: "default2",
                //    template: "{controller}/{action}/{productId:int}"
                //    );

                //routes.MapRoute(
                //    name: "editProduct",
                //    template: "{controller=Editor}/{action=EditProduct}/{productId?}"
                //    );

                //routes.MapRoute(
                //    name: "test",
                //    template: "{controller=Test}/{action=Run}"
                //    );  

                //routes.MapRoute(
                //    name: "register",
                //    template: "{controller=Auth}/{action=Register}"
                //    );


            });
        }
    }
}
