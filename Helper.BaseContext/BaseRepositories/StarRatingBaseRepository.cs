using Helper.BaseContext.Contexts;
using Helper.BaseContext.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Helper.BaseContext.BaseRepositories
{
    public class StarRatingBaseRepository : BaseRepository<StarRatingModel>
    {
        public StarRatingBaseRepository(ProjectDbBaseContext context, ILogger<StarRatingBaseRepository> logger)
            : base(context, logger)
        { }

        public StarRatingModel AddByDapper(StarRatingModel model)
        {
            try
            {
                Logger.LogInformation("[ Called: AddByDapper ]");
                model.Id = model.Id == Guid.Empty ? Guid.NewGuid() : model.Id;

                if(model.Products != null && model.Products.Any())
                {
                    Logger.LogWarning($"Você está usando dapper para essa ação! " +
                        $"A entidade '{nameof(model.Products)}' não está mapeada na consulta! " +
                        $"O relacionamento será descartado e um erro pode ser gerado.");
                }

                string sql = $@"INSERT INTO dbo.StarRating 
                    ([Id], [Star], [Image], [Description])
                    VALUES 
                    (@Id, @Star, @Image, @Description)";

                if (Execute(sql, model) == 1)
                {
                    return model;
                }

                throw new ArgumentNullException(nameof(model), $"O Modelo '{model.GetType().Name}' não foi adicionado!");
            }
            catch(Exception ex)
            {
                Logger.LogError(ex, "[ Called: AddByDapper ]");
                throw;
            }
        }
    }
}