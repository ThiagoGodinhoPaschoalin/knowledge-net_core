using ContextSample;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Poc.ThreadAndTask.Repositories;
using System;

namespace Poc.ThreadAndTask
{
    public class Startup
    {
        private ILogger<Startup> logger = default;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Projeto base que inicia conexão e fornece repositórios de exemplos para serem usados..
            services.AddSampleContext();

            services.AddScoped<ILocalRepository, LocalRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<Startup>();



            app.Use(async (context, next) =>
            {
                logger.LogInformation($"FIRST MIDDLEWARE: before\n");

                await next();

                logger.LogInformation($"FIRST MIDDLEWARE: after\n");
            });



            //Se na barra de endereço houver uma query com a chave 'branch', executar esse middleware;
            // http://localhost..../page?branch=xpto
            app.UseWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranchAndRejoin);



            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            app.UseAuthorization();



            app.UseExceptionHandler( builder => builder.Run(async context => 
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                logger.LogError(exception, $"\n\nHandlerException;\n\n");
                context.Response.Clear();
                context.Response.StatusCode = 409;
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.WriteAsync("<p>Houve uma falha na resposta e ela foi capturada aqui e alterada!</p>");
                return;
            }));



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void HandleBranchAndRejoin(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var branchVer = context.Request.Query["branch"];
                logger.LogInformation("HandleBranchAndRejoin: Before;\nBranch used = {branchVer}\n", branchVer);

                await next();

                logger.LogInformation("HandleBranchAndRejoin: After;\n");
            });
        }

    }
}
