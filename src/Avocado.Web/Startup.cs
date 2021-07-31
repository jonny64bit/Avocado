using Avocado.Base;
using Avocado.Base.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avocado.Base.Interfaces.FileProcessors;
using Avocado.Base.Services.FileProcessors;
using Avocado.Database;
using Avocado.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Avocado.Web
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
            ////////////////////////
            ////EF Core
            ////////////////////////
            services.AddDbContext<DAL>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            ////////////////////////
            ////ASP.NET Framework
            ////////////////////////
            services.AddControllersWithViews();

            ////////////////////////
            ////DI
            ////////////////////////
            SetupDependencyInjection(services);
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void SetupDependencyInjection(IServiceCollection services)
        {
            //Transient
            services.AddTransient<IService, Service>();
            services.AddTransient<IMeterReadingFileProcessor, MeterReadingFileProcessor>();

            //Auto Mapper
            services.AddAutoMapper(typeof(AutoMapperProfile));
        }
    }
}