using Poc.UOW.Models;
using Poc.UOW.Patterns;
using System;

namespace Poc.UOW.Business
{
    public class ProjectBusiness
    {
        private readonly UnitOfWorkRepository repository;

        public ProjectBusiness(UnitOfWorkRepository repository)
        {
            this.repository = repository;
        }

        public void Failure()
        {
            try
            {
                var newStarDp = repository.StarRatings.AddByDapper(new StarRatingModel
                {
                    Star = 1.1f,
                    Description = "1.1 Estrelas"
                });

                var newStarEF = repository.StarRatings.Add(new StarRatingModel
                {
                    Star = 2.1f,
                    Description = "2.1 Estrelas"
                });

                repository.Products.Add(new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A001",
                    Name = "Mouse sem Fio",
                    Price = 115.75m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarDp.Id
                });

                ///Ele vai falhar, pq o Id da 'starRating' do EF ainda está na staging.
                ///Repare que no Contexto, você encontrará a 'StarRating' criada no Dapper,
                ///mas não a criada no EF. 
                ///Uma forma simples e prática de explicar isso é:
                /// Ao executar o Dapper, já rola um 'SaveChanges()' implicito, que coloca seu resultado na stack do contexto;
                /// No EF, enquanto você não der SaveChanges, sua alteração está sendo 'trackeada', ainda no estado de DetectChanges,
                /// e somente após o 'SaveChanges()', que o EF adicionar sua alteração na stack do contexto;
                /// Obs.: O 'SaveChanges()' não persiste no banco, pq ele está contido em uma transação! 
                /// (Veja no UnitOfWorkRepository);
                repository.Products.AddByDapper(new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A002",
                    Name = "Mouse com Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarEF.Id//Exception Here!
                });

                repository.Commit();
            }
            catch
            {
                throw;
            }
        }

        public void Success()
        {
            try
            {
                var newStarDp = repository.StarRatings.AddByDapper(new StarRatingModel
                {
                    Star = 1.0f,
                    Description = "1.0 Estrelas"
                });

                var newStarEF = repository.StarRatings.Add(new StarRatingModel
                {
                    Star = 2.0f,
                    Description = "2.0 Estrelas"
                });
                ///Add to the Stack
                repository.Save();

                repository.Products.Add(new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A001",
                    Name = "Mouse sem Fio",
                    Price = 115.75m,
                    CreatedDate = DateTime.UtcNow,
                    StarRatingId = newStarDp.Id
                });
                ///Add to the Stack
                repository.Save();

                repository.Products.AddByDapper(new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A002",
                    Name = "Mouse com Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    //Can find in stack
                    StarRatingId = newStarEF.Id
                });

                repository.Products.Add(new ProductModel
                {
                    Id = Guid.NewGuid(),
                    Code = "A003",
                    Name = "Mouse e Teclado sem Fio",
                    Price = 60.50m,
                    CreatedDate = DateTime.UtcNow,
                    StarRating = new StarRatingModel
                    {
                        Star = 3.0f,
                        Description = "3.0 Estrelas"
                    }
                });

                repository.Commit();
            }
            catch
            {
                throw;
            }
        }
    }
}