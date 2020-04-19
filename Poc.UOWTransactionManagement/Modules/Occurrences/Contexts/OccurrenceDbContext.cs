using Microsoft.EntityFrameworkCore;
using Poc.Modules.Occurrences.Models;

namespace Poc.Modules.Occurrences.Contexts
{
    public class OccurrenceDbContext : DbContext
    {
        public OccurrenceDbContext(DbContextOptions<OccurrenceDbContext> options) 
            : base(options)
        { }

        public DbSet<OccurrenceModel> Occurrences { get; set; }
    }
}