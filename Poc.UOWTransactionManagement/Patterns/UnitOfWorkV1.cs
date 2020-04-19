using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Poc.Modules.Fruits.Contexts;
using Poc.Modules.Fruits.Models;
using Poc.Modules.Fruits.Repositories;
using Poc.Modules.Occurrences.Contexts;
using Poc.Modules.Occurrences.Models;
using Poc.Modules.Occurrences.Repositories;
using Poc.Modules.Persons.Contexts;
using Poc.Modules.Persons.Models;
using Poc.Modules.Persons.Repositories;

namespace Poc.UOWTransactionManagement.Patterns
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) 
            : base(options)
        { }
    }

    public class UnitOfWorkV1
    {
        private readonly TransactionDbContext transactionDbContext;

        public UnitOfWorkV1(TransactionDbContext transactionDbContext, ILoggerFactory loggerFactory)
        {
            this.transactionDbContext = transactionDbContext;
            var conn = this.transactionDbContext.Database.GetDbConnection();
            var tran = this.transactionDbContext.Database.BeginTransaction().GetDbTransaction();

            Fruits = UOWInstances.NewRepository<FruitDbContext, FruitRepository, FruitModel>(conn, tran, loggerFactory);
            Persons = UOWInstances.NewRepository<PersonDbContext, PersonRepository, PersonModel>(conn, tran, loggerFactory);
            Occurrences = UOWInstances.NewRepository<OccurrenceDbContext, OccurrenceRepository, OccurrenceModel>(conn, tran, loggerFactory);
        }

        public IFruitRepository Fruits { get; private set; }
        public IPersonRepository Persons { get; private set; }
        public IOccurrenceRepository Occurrences { get; private set; }
    }
}
