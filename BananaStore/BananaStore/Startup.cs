using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BananaStore;
using BananaStore.DataAccess.Entities;
using BananaStore.DataAccess.Repositories;
using BananaStore.Library.Interfaces;

namespace BananaStore
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

            string connectionString = Configuration.GetConnectionString("BannanaStoreDb");

            // among the services you register for DI (dependency injection)
            // should be your DbContext.
            services.AddDbContext<BananaStoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IProductsRepository, ProductRepository>();
            services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
            services.AddScoped<ILocationsRepository, LocationRepository>();
            services.AddScoped<ILocationStockRepository, LocationStockRepository>();

            services.AddControllersWithViews();
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
    }
}
