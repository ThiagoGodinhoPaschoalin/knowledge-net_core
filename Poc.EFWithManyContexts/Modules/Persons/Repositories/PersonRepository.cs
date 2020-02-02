using CoreLib;
using Microsoft.Extensions.Logging;
using Poc.EFWithManyContexts.Modules.Persons.Contexts;
using Poc.EFWithManyContexts.Modules.Persons.Models;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Persons.Repositories
{
    public class PersonRepository : BaseRepository<PersonModel>, IPersonRepository
    {
        public PersonRepository(PersonDbContext context, ILogger<PersonRepository> logger, IDbTransaction dbTransaction) 
            : base(context, logger, dbTransaction)
        { }
    }

    public interface IPersonRepository : IBaseRepository<PersonModel> 
    { }
}