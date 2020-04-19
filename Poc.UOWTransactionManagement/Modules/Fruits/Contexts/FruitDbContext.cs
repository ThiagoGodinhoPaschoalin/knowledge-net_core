using Microsoft.EntityFrameworkCore;
using Poc.Modules.Fruits.Models;

namespace Poc.Modules.Fruits.Contexts
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options) 
            : base(options)
        { }

        public DbSet<FruitModel> Fruits { get; set; }
    }
}