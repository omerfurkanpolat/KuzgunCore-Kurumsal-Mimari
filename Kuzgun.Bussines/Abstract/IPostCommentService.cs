using System;
using System.Collections.Generic;
using System.Text;

using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IPostCommentService
    {
        List<PostComment> GetAll();
        PostComment GetById(int id);
        void Create(PostComment entity);
        void Update(PostComment entity);
        void Delete(PostComment entity);
        List<PostComment> GetPostCommentRelatedEntitesByPostId(int postId);
        bool PostCommentExist(int userId, int postId);
    }
}
