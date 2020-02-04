using ContextsSample.Modules.Fruits.Contexts;
using ContextsSample.Modules.Fruits.Repositories;
using ContextsSample.Modules.Occurrences.Contexts;
using ContextsSample.Modules.Occurrences.Repositories;
using ContextsSample.Modules.Persons.Contexts;
using ContextsSample.Modules.Persons.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ContextsSample
{
    public static class DIServices
    {
        public static void AddSampleContexts(this IServiceCollection services)
        {
            services.AddScoped<IDbConnection>((serviceProvider) =>
            {
                return new SqlConnection(CoreLib.Constants.BaseConstants.GetConnectionString);
            });

            services.AddScoped<IDbTransaction>((serviceProvider) =>
            {
                var connection = serviceProvider.GetService<IDbConnection>();
                connection.Open();
                return connection.BeginTransaction(IsolationLevel.ReadUncommitted);
            });

            services.AddDbContextPool<PersonDbContext>((provider, options) =>
            {
                options.UseSqlServer((DbConnection)provider.GetService<IDbTransaction>().Connection);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<PersonRepository>();

            services.AddDbContextPool<FruitDbContext>((provider, options) =>
            {
                options.UseSqlServer((DbConnection)provider.GetService<IDbTransaction>().Connection);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<FruitRepository>();

            services.AddDbContextPool<OccurrenceDbContext>((provider, options) =>
            {
                options.UseSqlServer((DbConnection)provider.GetService<IDbTransaction>().Connection);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<OccurrenceRepository>();
        }
    }
}
