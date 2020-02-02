using ContextSample.Contexts;
using ContextSample.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContextSample
{
    public static class DIServices
    {
        public static void AddSampleContext(this IServiceCollection services)
        {
            services.AddDbContextPool<SampleDbContext>(opt =>
            {
                opt.UseSqlServer(CoreLib.Constants.BaseConstants.GetConnectionString);
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<SampleRepository>();
        }

        /// <summary>
        /// Executar migração atualizada
        /// </summary>
        /// <param name="app"></param>
        public static void UseSampleContextMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<SampleDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}