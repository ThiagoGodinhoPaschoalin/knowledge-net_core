using ContextSample.Models;
using Microsoft.EntityFrameworkCore;

namespace ContextSample.Contexts
{
    public sealed class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) 
            : base(options)
        { }

        public DbSet<SampleProductModel> Products { get; set; }
        public DbSet<SampleStarRatingModel> StarRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SampleProductModel>()
                .HasOne(x => x.StarRating)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.StarRatingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SampleStarRatingModel>()
                .HasIndex(x => x.Star)
                .HasName("UK_StarRating_Star")
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}