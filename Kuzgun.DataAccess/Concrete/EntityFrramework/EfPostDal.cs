using System.Collections.Generic;
using System.Linq;
using Kuzgun.Core.DataAccess.EntityFramework;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.DataAccess.Concrete.EntityFramework
{
    public class EfPostDal: EfEntityRepositoryBase<Post, KuzgunContext>, IPostDal
    {
        public List<Post> GetPostsRelatedEntites()
        {
            using (var context= new KuzgunContext())
            {
                return context.Posts.Include(p => p.SubCategory)
                    .Include(p => p.Category).ToList();
            }
        }
    }
}
