using ContextsSample.Modules.Fruits.Models;
using Microsoft.EntityFrameworkCore;

namespace ContextsSample.Modules.Fruits.Contexts
{
    public class FruitDbContext : DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options)
            : base(options)
        { }

        public DbSet<FruitModel> Fruits { get; set; }
    }
}