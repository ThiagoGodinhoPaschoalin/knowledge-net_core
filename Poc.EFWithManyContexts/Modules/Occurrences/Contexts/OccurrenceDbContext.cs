using Microsoft.EntityFrameworkCore;
using Poc.EFWithManyContexts.Modules.Occurrences.Models;
using Poc.EFWithManyContexts.Patterns;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Occurrences.Contexts
{
    public class OccurrenceDbContext : BaseDbContext
    {
        public OccurrenceDbContext(IDbConnection connection) 
            : base(connection)
        { }

        public DbSet<OccurrenceModel> Occurrences { get; set; }
    }
}