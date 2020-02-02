using CoreLib;
using Microsoft.Extensions.Logging;
using Poc.UOW.Contexts;
using Poc.UOW.Models;
using System;
using System.Data;

namespace Poc.UOW.Repositories
{
    public class StarRatingRepository : BaseRepository<StarRatingModel>
    {

        public StarRatingRepository(ProjectDbContext context, ILogger<StarRatingRepository> Logger, IDbTransaction dbTransaction)
            : base(context, Logger, dbTransaction)
        { }

        public StarRatingModel AddByDapper(StarRatingModel model)
        {
            try
            {
                model.Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;

                string sql = $@"INSERT INTO dbo.StarRating 
                    ([Id], [Star], [Image], [Description])
                    VALUES 
                    (@Id, @Star, @Image, @Description)";

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