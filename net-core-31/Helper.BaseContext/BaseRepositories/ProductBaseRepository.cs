using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Helper.BaseContext.Models;
using Helper.BaseContext.Contexts;
using System;
using Microsoft.Extensions.Logging;

namespace Helper.BaseContext.BaseRepositories
{
    public class ProductBaseRepository : BaseRepository<ProductModel>
    {
        public ProductBaseRepository(ProjectDbBaseContext context, ILogger<ProductBaseRepository> Logger)
            : base(context, Logger)
        { }

        public override IEnumerable<ProductModel> GetAll()
        {
            try
            {
                Logger.LogDebug("[ Called: GetAll ]");

                return GetModel.Include(x => x.StarRating)?.AsNoTracking().ToList();
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, $"[ Called: GetAll ] ");
                throw;
            }
            
        }

        public object PrintAll()
        {
            try
            {
                Logger.LogDebug("[ Called: PrintAll ]");

                return GetAll().Select(x => new
                {
                    x.Name,
                    x.Code,
                    x.Price,
                    x.Description,
                    StarRating = new
                    {
                        x.StarRating.Star,
                        x.StarRating.Description
                    }
                });
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "[ Called: PrintAll ]");
                throw;
            }
        }

        public ProductModel AddByDapper(ProductModel model)
        {
            try
            {
                Logger.LogDebug("[ Called: AddByDapper ]");

                model.Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;

                if(model?.StarRating != null)
                {
                    Logger.LogWarning($"Você está usando dapper para essa ação! " +
                        $"E a entidade '{nameof(model.StarRating)}' não está mapeada na consulta! " +
                        $"O relacionamento será descartado e um erro pode ser gerado.");
                }

                string sql = $@"INSERT INTO dbo.Products 
                    ([Id], [Name], [Code], [CreatedDate], [Image], [Price], [StarRatingId])
                    VALUES 
                    (@Id, @Name, @Code, @CreatedDate, @Image, @Price, @StarRatingId);";

                if (Execute(sql, model) == 1)
                {
                    return model;
                }

                throw new ArgumentNullException(nameof(model), $"O Modelo '{model.GetType().Name}' não foi adicionado!");
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, $"[ Called: AddByDapper ] ");
                throw;
            }
        }
    }
}