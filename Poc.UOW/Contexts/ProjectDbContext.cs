using Microsoft.EntityFrameworkCore;
using Poc.UOW.Models;

namespace Poc.UOW.Contexts
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) 
            : base(options)
        { }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<StarRatingModel> StarRatings { get; set; }
    }
}