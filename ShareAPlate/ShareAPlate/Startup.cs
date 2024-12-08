using Microsoft.AspNetCore.Mvc.Authorization;
using ShareAPlate.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ShareAPlate
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
            // Add services to the container.
            services.AddControllersWithViews();

            // Add DBContext to the container
            services.AddDbContext<ShareAPlateContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        } 

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}");
            });
        }
    }
}
