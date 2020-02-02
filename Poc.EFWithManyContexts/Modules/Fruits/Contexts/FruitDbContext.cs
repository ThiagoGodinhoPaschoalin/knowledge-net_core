using Microsoft.EntityFrameworkCore;
using Poc.EFWithManyContexts.Modules.Fruits.Models;
using Poc.EFWithManyContexts.Patterns;
using System.Data;

namespace Poc.EFWithManyContexts.Modules.Fruits.Contexts
{
    public class FruitDbContext : BaseDbContext
    {
        public FruitDbContext(IDbConnection connection) 
            : base(connection)
        { }

        public DbSet<FruitModel> Fruits { get; set; }
    }
}