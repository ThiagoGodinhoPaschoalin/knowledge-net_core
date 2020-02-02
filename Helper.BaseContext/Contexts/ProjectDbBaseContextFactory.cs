using Helper.BaseContext.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Helper.BaseContext.Contexts
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
    public class ProjectDbBaseContextFactory : IDesignTimeDbContextFactory<ProjectDbBaseContext>
    {
        public ProjectDbBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDbBaseContext>();

            optionsBuilder.UseSqlServer(BaseConstants.GetConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();

            return new ProjectDbBaseContext(optionsBuilder.Options);
        }
    }
}