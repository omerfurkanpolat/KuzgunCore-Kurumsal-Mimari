using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.DataAccess;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.DataAccess.Abstract
{
    public interface IPostCommentDal : IEntityRepository<PostComment>
    {
        List<PostComment> GetPostsCommentRelatedEntites();
    }
}
