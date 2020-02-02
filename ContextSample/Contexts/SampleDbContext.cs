using ContextSample.Models;
using Microsoft.EntityFrameworkCore;
using System;

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

            Guid[] starRatingIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            modelBuilder
                .Entity<SampleStarRatingModel>()
                .HasData(SeedStar(starRatingIds));
            modelBuilder
                .Entity<SampleProductModel>()
                .HasData(SeedProducts(starRatingIds));

            base.OnModelCreating(modelBuilder);
        }

        private SampleStarRatingModel[] SeedStar(Guid[] starRatingIds)
        {
            SampleStarRatingModel[] stars = new SampleStarRatingModel[]
            {
                new SampleStarRatingModel
                {
                    Id = starRatingIds[0],
                    Star = 0.1f
                },
                new SampleStarRatingModel
                     {
                         Id = starRatingIds[1],
                         Star = 0.2f
                     },
                new SampleStarRatingModel
                     {
                         Id = starRatingIds[2],
                         Star = 0.3f
                     },
                new SampleStarRatingModel
                     {
                         Id = starRatingIds[3],
                         Star = 0.4f
                     },
                new SampleStarRatingModel
                     {
                         Id = starRatingIds[4],
                         Star = 0.5f
                     },
            };
            return stars;
        }
        private SampleProductModel[] SeedProducts(Guid[] starRatingIds)
        {
            SampleProductModel[] products = new SampleProductModel[]
            {
                new SampleProductModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Leaf Rake",
                    Code = "GDN-0011",
                    CreatedDate = new DateTime(2019, 03, 19),
                     Description = "Leaf rake with 48-inch wooden handle.",
                     Price = 19.95m,
                     StarRatingId = starRatingIds[0],
                     Image = "assets/images/leaf_rake.png"
                },
                new SampleProductModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Garden Cart",
                    Code = "GDN-0023",
                    CreatedDate = new DateTime(2019, 03, 18),
                     Description = "15 gallon capacity rolling garden cart",
                     Price = 32.99m,
                     StarRatingId = starRatingIds[1],
                     Image = "assets/images/garden_cart.png"
                },
                new SampleProductModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Hammer",
                    Code = "TBX-0048",
                    CreatedDate = new DateTime(2019, 05, 21),
                     Description = "Curved claw steel hammer",
                     Price = 8.9m,
                     StarRatingId = starRatingIds[2],
                     Image = "assets/images/hammer.png"
                },
                new SampleProductModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Saw",
                    Code = "TBX-0022",
                    CreatedDate = new DateTime(2019, 05, 15),
                     Description = "15-inch steel blade hand saw",
                     Price = 11.55m,
                     StarRatingId = starRatingIds[3],
                     Image = "assets/images/saw.png"
                },
                new SampleProductModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Video Game Controller",
                    Code = "GMG-0042",
                    CreatedDate = new DateTime(2019, 10, 15),
                     Description = "Standard two-button video game controller",
                     Price = 35.95m,
                     StarRatingId = starRatingIds[4],
                     Image = "assets/images/xbox-controller.png"
                },
            };

            return products;
        }
    }
}