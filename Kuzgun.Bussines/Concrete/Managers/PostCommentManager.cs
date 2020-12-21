using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kuzgun.Bussines.Abstract;

using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class PostCommentManager:IPostCommentService
    {
        private IPostCommentDal _postCommentDal;

        public PostCommentManager(IPostCommentDal postCommentDal)
        {
            _postCommentDal = postCommentDal;
        }

        public List<PostComment> GetAll()
        {
            return _postCommentDal.GetList();
        }

        public PostComment GetById(int id)
        {
            return _postCommentDal.Get(pc => pc.PostCommentId == id);
        }

        public void Create(PostComment entity)
        {
            _postCommentDal.Add(entity);
        }

        public void Update(PostComment entity)
        {
            _postCommentDal.Update(entity);
        }

        public void Delete(PostComment entity)
        {
            _postCommentDal.Delete(entity);
        }

        public List<PostComment> GetPostCommentRelatedEntitesByPostId(int postId)
        {
            return _postCommentDal.GetPostsCommentRelatedEntites().Where(ps => ps.PostId==postId).ToList();
        }

        public bool PostCommentExist(int userId, int postId)
        {
            return _postCommentDal.GetList().Where(pc => pc.UserId == userId && pc.PostId == postId).Count() > 0;
        }
    }
}
