using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using CoreLib;
using System.Data;
using ContextSample.Contexts;
using ContextSample.Models;

namespace ContextSample.Repositories
{
    public class SampleProductRepository : BaseRepository<SampleProductModel>
    {
        public SampleProductRepository(SampleDbContext context, ILogger<SampleProductRepository> Logger, IDbTransaction dbTransaction)
            : base(context, Logger, dbTransaction)
        { }

        public override IEnumerable<SampleProductModel> GetAll()
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

        public SampleProductModel AddByDapper(SampleProductModel model)
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