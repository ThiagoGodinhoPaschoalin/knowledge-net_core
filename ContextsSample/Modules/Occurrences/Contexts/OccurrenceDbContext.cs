using ContextsSample.Modules.Occurrences.Models;
using Microsoft.EntityFrameworkCore;

namespace ContextsSample.Modules.Occurrences.Contexts
{
    public class OccurrenceDbContext : DbContext
    {
        public OccurrenceDbContext(DbContextOptions<OccurrenceDbContext> options)
            : base(options)
        { }

        public DbSet<OccurrenceModel> Occurrences { get; set; }
    }
}