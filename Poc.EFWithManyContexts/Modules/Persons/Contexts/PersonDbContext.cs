using Microsoft.EntityFrameworkCore;
using Poc.EFWithManyContexts.Modules.Persons.Models;
using Poc.EFWithManyContexts.Patterns;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Persons.Contexts
{
    public class PersonDbContext : BaseDbContext
    {
        public PersonDbContext(IDbConnection connection) 
            : base(connection)
        { }

        public DbSet<PersonModel> Peoples { get; set; }
    }
}