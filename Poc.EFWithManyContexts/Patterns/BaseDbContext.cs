using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Poc.EFWithManyContexts.Patterns
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly DbConnection connection;

        protected BaseDbContext(IDbConnection connection)
        {
            this.connection = connection as DbConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection, (sql) => 
            {
                sql.CommandTimeout(15); 
            });

            optionsBuilder.EnableSensitiveDataLogging();
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}