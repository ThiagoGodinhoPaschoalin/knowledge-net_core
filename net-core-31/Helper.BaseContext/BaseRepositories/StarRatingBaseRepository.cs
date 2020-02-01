using Helper.BaseContext.Contexts;
using Helper.BaseContext.Models;

namespace Helper.BaseContext.BaseRepositories
{
    public class StarRatingBaseRepository : BaseRepository<StarRatingModel>
    {
        public StarRatingBaseRepository(ProjectDbBaseContext context)
            : base(context)
        { }
    }
}