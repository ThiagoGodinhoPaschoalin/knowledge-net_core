using CoreLib;
using Microsoft.Extensions.Logging;
using Poc.EFWithManyContexts.Modules.Fruits.Contexts;
using Poc.EFWithManyContexts.Modules.Fruits.Models;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Fruits.Repositories
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