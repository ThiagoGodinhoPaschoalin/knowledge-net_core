using Microsoft.Extensions.Logging;
using Poc.Modules.Fruits.Contexts;
using Poc.Modules.Fruits.Models;
using Poc.UOWTransactionManagement.Patterns;

namespace Poc.Modules.Fruits.Repositories
{
    public class FruitRepository : BaseRepository<FruitModel, FruitDbContext>, IFruitRepository
    {
        public FruitRepository(FruitDbContext context, ILogger<FruitRepository> logger) 
            : base(context, logger)
        { }
    }

    public interface IFruitRepository : IBaseRepository<FruitModel>
    { }
}