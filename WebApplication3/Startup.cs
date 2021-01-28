using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication3.Hubs;
using WebApplication3.Models;
using WebApplication3.Models.Hubs;

namespace WebApplication3
{
    public class Startup
    {
        public IConfiguration Configuration { get;  }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSignalR();
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDeveloperExceptionPage(); // informacje szczegó³owe o b³êdach
            app.UseStatusCodePages(); // Wyœwietla strony ze statusem b³êdu
            app.UseStaticFiles(); // obs³uga treœci statycznych css, images, js
            app.UseElapsedTimeMiddleware();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");
                routes.MapHub<VisitorCounterHub>("/counter");
                routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=List}/{id?}");
                

                routes.MapControllerRoute(
                name: "null",
                pattern: "Product/{category}",
                defaults: new
                {
                    controller = "Product",
                    action = "List",
                });
                routes.MapControllerRoute(
                name: null,
                pattern: "Admin/{action}",
                defaults: new
                {
                controller = "Admin",
                action = "Index"
                });
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
