using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Poc.UOW.Business;
using Poc.UOW.Contexts;
using Poc.UOW.Patterns;

namespace Poc.UOW
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
            services.AddControllers();

            services.AddDbContextPool<ProjectDbContext>(opt =>
            {
                ///TODO: You need add a 'DefaultConnection in 'ConnectionStrings' inside 'appsettings.json';
                ///

                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<UnitOfWorkRepository>();
            services.AddScoped<ProjectBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



        /// <summary>
        /// effect: dotnet ef database update
        /// </summary>
        /// <param name="app"></param>
        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ProjectDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
