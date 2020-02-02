using Helper.BaseContext.BaseRepositories;
using Helper.BaseContext.Constants;
using Helper.BaseContext.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Helper.BaseContext
{
    public static class DIServices
    {
        public static void AddBaseContext(this IServiceCollection services, bool isUOW = false)
        {
            services.AddDbContextPool<ProjectDbBaseContext>(opt =>
            {
                ///TODO: You need add a 'DefaultConnection in 'ConnectionStrings' inside 'appsettings.json';
                ///

                opt.UseSqlServer(BaseConstants.GetConnectionString);
                opt.EnableSensitiveDataLogging();
            });

            if (!isUOW)
            {
                services.AddScoped<ProductBaseRepository>();
                services.AddScoped<StarRatingBaseRepository>();
            }
        }

        /// <summary>
        /// Executar migração atualizada
        /// </summary>
        /// <param name="app"></param>
        public static void UseBaseContextMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ProjectDbBaseContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}