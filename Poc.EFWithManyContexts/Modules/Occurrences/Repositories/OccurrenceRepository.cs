using CoreLib;
using Microsoft.Extensions.Logging;
using Poc.EFWithManyContexts.Modules.Occurrences.Contexts;
using Poc.EFWithManyContexts.Modules.Occurrences.Models;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Occurrences.Repositories
{
    public class OccurrenceRepository : BaseRepository<OccurrenceModel>, IOccurrenceRepository
    {
        public OccurrenceRepository(OccurrenceDbContext context, ILogger<OccurrenceRepository> logger, IDbTransaction dbTransaction = null) 
            : base(context, logger, dbTransaction)
        { }
    }

    public interface IOccurrenceRepository : IBaseRepository<OccurrenceModel>
    { }
}