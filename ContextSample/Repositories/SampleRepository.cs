using ContextSample.Contexts;
using ContextSample.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;

namespace ContextSample.Repositories
{
    public class SampleRepository
    {
        private readonly SampleDbContext context;
        private readonly ILoggerFactory loggerFactory;

        private readonly ILogger<SampleRepository> logger;

        public SampleRepository(SampleDbContext context, ILoggerFactory loggerFactory)
        {
            this.context = context;
            this.context.Database.BeginTransaction();

            this.loggerFactory = loggerFactory;

            logger = this.loggerFactory.CreateLogger<SampleRepository>();
        }

        public SampleProductRepository Products
        {
            get
            {
                logger.LogInformation("[Call Property Products]");

                var productLogger = loggerFactory.CreateLogger<SampleProductRepository>();
                var transaction = context.Database.CurrentTransaction.GetDbTransaction();
                return new SampleProductRepository(context, productLogger, transaction);
            }
        }

        public SampleStarRatingRepository StarRating
        {
            get
            {
                logger.LogInformation("[Call Property StarRating]");

                var starLogger = loggerFactory.CreateLogger<SampleStarRatingRepository>();
                var transaction = context.Database.CurrentTransaction.GetDbTransaction();
                return new SampleStarRatingRepository(context, starLogger, transaction);
            }
        }



        public void Commit()
        {
            logger.LogInformation("[ Commit ]");

            try
            {
                context.SaveChanges();
                context.Database.CommitTransaction();
                logger.LogInformation("[ Successfully Commited ]");
            }
            catch(Exception ex)
            {
                logger.LogWarning(ex, "[ Exception in commit. Rollback is called ]");
                context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                logger.LogInformation("[ Begin a new transaction ]");
                context.Database.BeginTransaction();
            }
        }

        public void SeedStartedData()
        {
            try
            {
                logger.LogInformation("[ Seed Started Data ]");
                #region Seed
                Guid[] starRatingIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

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
                #endregion

                StarRating.AddRange(stars);
                Products.AddRange(products);

                context.SaveChanges();
                context.Database.CommitTransaction();

                logger.LogInformation("[ Seed Started Data ]: [ Successfully Commited ]");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "[ Seed Started Data ]: [ Exception in commit. Rollback is called ]");
                context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                logger.LogInformation("[ Seed Started Data ]: [ Begin a new transaction ]");

                context.Database.BeginTransaction();
            }
        }
    }
}
