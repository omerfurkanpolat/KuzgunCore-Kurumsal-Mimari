using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IPostCommentService
    {
        IDataResult<List<PostComment>> GetAll();
        IDataResult<PostComment> GetById(int id);
        IResult Create(PostComment entity);
        IResult Update(PostComment entity);
        IResult Delete(PostComment entity);
        IDataResult<List<PostComment>> GetPostCommentRelatedEntitiesByPostId(int postId);
        IDataResult< bool> PostCommentExist(int userId, int postId);
    }
}
