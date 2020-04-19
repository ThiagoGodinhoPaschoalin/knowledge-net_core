using Microsoft.EntityFrameworkCore;
using Poc.Modules.Persons.Models;

namespace Poc.Modules.Persons.Contexts
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) 
            : base(options)
        { }

        public DbSet<PersonModel> Peoples { get; set; }
    }
}