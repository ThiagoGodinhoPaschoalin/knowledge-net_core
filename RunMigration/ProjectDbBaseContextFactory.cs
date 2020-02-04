using ContextSample.Contexts;
using ContextsSample.Modules.Fruits.Contexts;
using ContextsSample.Modules.Occurrences.Contexts;
using ContextsSample.Modules.Persons.Contexts;
using CoreLib.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace RunMigration
{
    /// <summary>
    /// https://docs.microsoft.com/pt-br/ef/core/miscellaneous/cli/dbcontext-creation
    /// https://codingblast.com/entityframework-core-add-implementation-idesigntimedbcontextfactory-multiple-dbcontexts/
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
    public abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
    {
        public T CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<T>();

            builder.UseSqlServer(BaseConstants.GetConnectionString);
            builder.EnableSensitiveDataLogging();

            return (T) Activator.CreateInstance(typeof(T), builder.Options);
        }
    }

    /// <summary>
    /// dotnet ef migrations add Initial -p ..\ContextSample\ContextSample.csproj
    /// dotnet ef database update -p ..\ContextSample\ContextSample.csproj
    /// </summary>
    public class SampleDesignTimeDbContextFactory : DesignTimeDbContextFactory<SampleDbContext>
    { }

    /// <summary>
    /// dotnet ef migrations add InitialPerson -p ..\ContextsSample\ContextsSample.csproj -c PersonDbContext
    /// dotnet ef database update -p ..\ContextsSample\ContextsSample.csproj -c PersonDbContext
    /// </summary>
    public class PersonDesignTimeDbContextFactory : DesignTimeDbContextFactory<PersonDbContext> 
    { }

    /// <summary>
    /// dotnet ef migrations add InitialFruit -p ..\ContextsSample\ContextsSample.csproj -c FruitDbContext
    /// dotnet ef database update -p ..\ContextsSample\ContextsSample.csproj -c FruitDbContext
    /// </summary>
    public class FruitDesignTimeDbContextFactory : DesignTimeDbContextFactory<FruitDbContext>
    { }

    /// <summary>
    /// dotnet ef migrations add InitialOccurrence -p ..\ContextsSample\ContextsSample.csproj -c OccurrenceDbContext
    /// dotnet ef database update -p ..\ContextsSample\ContextsSample.csproj -c OccurrenceDbContext
    /// </summary>
    public class OccurrenceDesignTimeDbContextFactory : DesignTimeDbContextFactory<OccurrenceDbContext>
    { }
}