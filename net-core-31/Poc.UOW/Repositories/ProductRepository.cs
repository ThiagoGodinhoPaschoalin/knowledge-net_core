using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Poc.UOW.Contexts;
using Poc.UOW.Models;
using Poc.UOW.Patterns;
using System;

namespace Poc.UOW.Repositories
{
    public class ProductRepository : BaseRepository<ProductModel>
    {

        public ProductRepository(ProjectDbContext context)
            : base(context)
        { }

        public override IEnumerable<ProductModel> GetAll()
        {
            return GetModel.Include(x => x.StarRating)?.ToList();
        }

        public ProductModel AddByDapper(ProductModel model)
        {
            try
            {
                model.Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;

                string sql = $@"INSERT INTO dbo.Products 
                    ([Id], [Name], [Code], [CreatedDate], [Image], [Price], [StarRatingId])
                    VALUES 
                    (@Id, @Name, @Code, @CreatedDate, @Image, @Price, @StarRatingId);";

                if(Execute(sql, model) == 1)
                {
                    return model;
                }

                throw new ArgumentNullException(nameof(model), $"O Modelo '{model.GetType().Name}' não foi adicionado!");
            }
            catch
            {
                throw;
            }
        }
    }
}
