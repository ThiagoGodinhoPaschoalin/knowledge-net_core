using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Helper.BaseContext.Models;
using Helper.BaseContext.Contexts;

namespace Helper.BaseContext.BaseRepositories
{
    public class ProductBaseRepository : BaseRepository<ProductModel>
    {
        public ProductBaseRepository(ProjectDbBaseContext context)
            : base(context)
        { }

        public override IEnumerable<ProductModel> GetAll()
        {
            return GetModel.Include(x => x.StarRating)?.AsNoTracking().ToList();
        }

        public object PrintAll()
        {
            try
            {
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
            catch
            {
                throw;
            }
        }
    }
}