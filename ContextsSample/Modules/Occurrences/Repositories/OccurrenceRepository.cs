using ContextsSample.Modules.Occurrences.Contexts;
using ContextsSample.Modules.Occurrences.Models;
using CoreLib;
using Microsoft.Extensions.Logging;
using System.Data;

namespace ContextsSample.Modules.Occurrences.Repositories
{
    public class OccurrenceRepository : BaseRepository<OccurrenceModel>, IOccurrenceRepository
    {
        public OccurrenceRepository(OccurrenceDbContext context, ILogger<OccurrenceRepository> logger, IDbTransaction dbTransaction) 
            : base(context, logger, dbTransaction)
        { }
    }

    public interface IOccurrenceRepository : IBaseRepository<OccurrenceModel>
    { }
}