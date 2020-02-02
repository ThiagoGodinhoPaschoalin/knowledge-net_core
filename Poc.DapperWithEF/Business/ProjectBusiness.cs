using Poc.DapperWithEF.Contexts;
using Poc.DapperWithEF.Repositories;
using System;

namespace Poc.DapperWithEF.Business
{
    public class ProjectBusiness
    {
        private readonly ProjectDbContext context;

        private readonly ProductRepository products;
        private readonly StarRatingRepository starRatings;

        public ProjectBusiness(ProjectDbContext context, ProductRepository product, StarRatingRepository starRating)
        {
            this.context = context;
            this.products = product;
            this.starRatings = starRating;
        }

        public void Failure()
        {
            try
            {
                var newStarDp = starRatings.AddByDapper(new Models.StarRatingModel
                {
                    Star = 1.0f,
                    Description = "1.0 Estrelas"
                });

                var newStarEF = starRatings.Add(new Models.StarRatingModel
                {
                    Star = 2.0f,
                    Description = "2.0 Estrelas"
                });
                //context.SaveChanges();

                products.Add(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A001",
                    Name = "Mouse sem Fio",
                    Price = 115.75m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarDp.Id
                });
                //context.SaveChanges();

                products.AddByDapper(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A002",
                    Name = "Mouse com Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarEF.Id//Exception Here!
                });

                products.Add(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A003",
                    Name = "Mouse e Teclado sem Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRating = new Models.StarRatingModel
                    {
                        Star = 3.0f,
                        Description = "3.0 Estrelas"
                    }
                });
                //context.SaveChanges();

                context.Database.CommitTransaction();
            }
            catch
            {
                context.Database.RollbackTransaction();
                throw;
            }
        }

        public void Success()
        {
            try
            {
                var newStarDp = starRatings.AddByDapper(new Models.StarRatingModel
                {
                    Star = 1.0f,
                    Description = "1.0 Estrelas"
                });

                var newStarEF = starRatings.Add(new Models.StarRatingModel
                {
                    Star = 2.0f,
                    Description = "2.0 Estrelas"
                });
                context.SaveChanges();

                products.Add(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A001",
                    Name = "Mouse sem Fio",
                    Price = 115.75m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarDp.Id
                });
                context.SaveChanges();

                products.AddByDapper(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A002",
                    Name = "Mouse com Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarEF.Id
                });

                products.Add(new Models.ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A003",
                    Name = "Mouse e Teclado sem Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRating = new Models.StarRatingModel
                    {
                        Star = 3.0f,
                        Description = "3.0 Estrelas"
                    }
                });
                context.SaveChanges();

                context.Database.CommitTransaction();
            }
            catch
            {
                context.Database.RollbackTransaction();
                throw;
            }
        }
    }
}