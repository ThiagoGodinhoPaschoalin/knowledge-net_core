using ContextsSample.Modules.Persons.Contexts;
using ContextsSample.Modules.Persons.Models;
using CoreLib;
using Microsoft.Extensions.Logging;
using System.Data;

namespace ContextsSample.Modules.Persons.Repositories
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