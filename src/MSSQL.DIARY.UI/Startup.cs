using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSSQL.DIARY.UI.Local_db;
using MSSQL.DIARY.UI.Local_db.Seed;
using Nito.AsyncEx;

namespace MSSQL.DIARY.UI
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
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options =>  
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                );
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            Configuration.SetServerInformations();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder => {
                builder.AllowAnyOrigin().AllowAnyHeader();
                });
             
            app.UseMvc();
            app.UseExceptionHandler("/Error");
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            services.SeedData();
           // AsyncContext.Run(services.SeedData);
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            if (env.IsDevelopment())
                app.UseSpa(spa =>
                {

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer("start");
                        // spa.UseProxyToSpaDevelopmentServer("https://localhost:4200");
                        spa.Options.StartupTimeout = TimeSpan.FromSeconds(200); // <-- add this line
                    }
                });

        }
    }
}