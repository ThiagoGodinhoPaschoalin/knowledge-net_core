using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace Poc.UOWTransactionManagement.Patterns
{
    public static class UOWInstances
    {
        /// <summary>
        /// Criar uma nova instância de DbContext
        /// </summary>
        /// <typeparam name="TContext">your DbContext type</typeparam>
        /// <param name="context">your DbContext</param>
        /// <param name="dbConnection">required</param>
        /// <param name="dbTransaction">optional</param>
        /// <returns></returns>
        public static TContext NewInstance<TContext>(DbConnection dbConnection, DbTransaction dbTransaction)
            where TContext : DbContext
        {
            if(dbConnection is null)
            {
                throw new ArgumentNullException(nameof(dbConnection), "An existing connection is required.");
            }

            DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>()
                .EnableSensitiveDataLogging()
                .UseSqlServer(dbConnection, opt =>
                {
                    opt.MigrationsHistoryTable(typeof(TContext).Name.ToLowerInvariant(), "tgp_migrations");
                });

            var context = (TContext) Activator.CreateInstance(typeof(TContext), builder.Options);

            Console.WriteLine($"[ {typeof(TContext).Name} ]::CanConnect(): '{context.Database.CanConnect()}'");

            if (context.Database.CanConnect())
            {
                context.Database.Migrate();
            }

            if (dbTransaction != null)
            {
                Console.WriteLine("\n\n");
                Console.WriteLine($"[ {typeof(TContext).Name} ]::GetDbTransaction(): {context.Database.CurrentTransaction.GetDbTransaction()}");
                Console.WriteLine($"New DbTransaction: {dbTransaction}");
                Console.WriteLine($"Equals: {context.Database.CurrentTransaction.GetDbTransaction() == dbTransaction}");
                Console.WriteLine("\n\n");

                context.Database.UseTransaction(dbTransaction);
            }

            return context;
        }


        public static TRepository NewRepository<TContext, TRepository, TModel>
            (DbConnection dbConnection, DbTransaction dbTransaction, ILoggerFactory loggerFactory)
            where TModel : class, new()
            where TRepository : BaseRepository<TModel, TContext>
            where TContext : DbContext
        {
            var context = NewInstance<TContext>(dbConnection, dbTransaction);
            //TContext context, ILogger<TModel> logger
            return (TRepository) Activator.CreateInstance(typeof(TRepository), context, loggerFactory.CreateLogger<TRepository>());
        }

        /*
            private OccurrenceDbContext occurrenceDbContext;
        private OccurrenceRepository occurrenceRepository;
        public IOccurrenceRepository Occurrences
        {
            get
            {
                if (NeedChangeTransaction(nameof(Occurrences)))
                {
                    occurrenceDbContext = new OccurrenceDbContext(dbConnection);
                    occurrenceRepository = new OccurrenceRepository
                        (occurrenceDbContext, loggerFactory.CreateLogger<OccurrenceRepository>(), dbTransaction);
                }

                return occurrenceRepository;
            }
        }
        */
    }
}
