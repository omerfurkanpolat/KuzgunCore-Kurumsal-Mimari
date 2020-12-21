using System.Collections.Generic;
using System.Linq;
using Kuzgun.Core.DataAccess.EntityFramework;

using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Kuzgun.DataAccess.Concrete.EntityFramework
{
    public class EfPostCommentDal: EfEntityRepositoryBase<PostComment, KuzgunContext>, IPostCommentDal
    {
        public List<PostComment> GetPostsCommentRelatedEntites()
        {
            using (var context= new KuzgunContext())
            {
                return context.PostComments.Include(ps => ps.User).ToList();
            }
        }
    }
}
