using ContextsSample.Modules.Persons.Models;
using Microsoft.EntityFrameworkCore;

namespace ContextsSample.Modules.Persons.Contexts
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        { }

        public DbSet<PersonModel> Peoples { get; set; }
    }
}