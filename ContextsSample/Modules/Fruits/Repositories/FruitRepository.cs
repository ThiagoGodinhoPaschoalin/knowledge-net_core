using ContextsSample.Modules.Fruits.Contexts;
using ContextsSample.Modules.Fruits.Models;
using CoreLib;
using Microsoft.Extensions.Logging;
using System.Data;

namespace ContextsSample.Modules.Fruits.Repositories
{
    public class FruitRepository : BaseRepository<FruitModel>, IFruitRepository
    {
        public FruitRepository(FruitDbContext context, ILogger<FruitRepository> logger, IDbTransaction dbTransaction) 
            : base(context, logger, dbTransaction)
        { }
    }

    public interface IFruitRepository : IBaseRepository<FruitModel>
    { }
}