using Helper.BaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poc.ThreadAndTask.Repositories;

namespace Poc.ThreadAndTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Projeto base que inicia conexão e fornece repositórios de exemplos para serem usados..
            services.AddBaseContext();



            services.AddScoped<ILocalRepository, LocalRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //Projeto base que compara e preenche os dados do banco de dados.
            app.UseBaseContextMigration();


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
    }
}
