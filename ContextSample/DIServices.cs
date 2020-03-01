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


            using var scope = services.BuildServiceProvider().CreateScope();
            using var context = scope.ServiceProvider.GetService<SampleDbContext>();
            context.Database.Migrate();
        }
    }
}