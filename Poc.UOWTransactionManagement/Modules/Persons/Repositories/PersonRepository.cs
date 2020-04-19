using Microsoft.Extensions.Logging;
using Poc.Modules.Persons.Contexts;
using Poc.Modules.Persons.Models;
using Poc.UOWTransactionManagement.Patterns;

namespace Poc.Modules.Persons.Repositories
{
    public class PersonRepository : BaseRepository<PersonModel, PersonDbContext>, IPersonRepository
    {
        public PersonRepository(PersonDbContext context, ILogger<PersonRepository> logger) 
            : base(context, logger)
        { }
    }

    public interface IPersonRepository : IBaseRepository<PersonModel> 
    { }
}