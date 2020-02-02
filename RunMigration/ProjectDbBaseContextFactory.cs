using ContextSample.Contexts;
using CoreLib.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RunMigration
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/ef/core/miscellaneous/cli/dbcontext-creation
    /// </summary>
    /// <remarks>
    /// 
    ///     Agora eu posso simplesmente executar o comando 'dotnet ef migrations add Xpto' a partir desta biblioteca para criar migrações independentes!
    ///     Não sou obrigado a ter um MVC ou uma API com um Startup para isso.
    /// 
    /// </remarks>
    /// <example>
    /// 
    ///     dotnet ef migrations add Initial -p ..\ContextSample\ContextSample.csproj
    ///     dotnet ef database update -p ..\ContextSample\ContextSample.csproj
    /// 
    /// </example>
    public class ProjectDbBaseContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
    {
        public SampleDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>();

            optionsBuilder.UseSqlServer(BaseConstants.GetConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();

            return new SampleDbContext(optionsBuilder.Options);
        }
    }
}