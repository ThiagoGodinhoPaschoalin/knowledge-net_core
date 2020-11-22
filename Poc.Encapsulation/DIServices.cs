using Microsoft.Extensions.DependencyInjection;
using Poc.Encapsulation.ExampleLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poc.Encapsulation
{
    public static class DIServices
    {
        public static void AddLayers(this IServiceCollection services)
        {
            services.AddScoped<ConnectionLayer>();
            services.AddScoped<RepositoryLayer>();
            services.AddScoped<BusinessLayer>();
        }
    }
}
