using Microsoft.Extensions.Logging;
using Poc.Modules.Occurrences.Contexts;
using Poc.Modules.Occurrences.Models;
using Poc.UOWTransactionManagement.Patterns;

namespace Poc.Modules.Occurrences.Repositories
{
    public class OccurrenceRepository : BaseRepository<OccurrenceModel, OccurrenceDbContext>, IOccurrenceRepository
    {
        public OccurrenceRepository(OccurrenceDbContext context, ILogger<OccurrenceRepository> logger) 
            : base(context, logger)
        { }
    }

    public interface IOccurrenceRepository : IBaseRepository<OccurrenceModel>
    { }
}