using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Internal;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
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

        public IDataResult <List<PostComment>> GetAll()
        {
            var result = _postCommentDal.GetList();
            if (result==null)
            {
                return new ErrorDataResult<List<PostComment>>(Messages.PostCommentsNotFound);
            }
            return new SuccessDataResult<List<PostComment>>(result);
        }

        public IDataResult<PostComment> GetById(int id)
        {
            var result= _postCommentDal.Get(pc => pc.PostCommentId == id);
            if (result==null)
            {
                return new ErrorDataResult<PostComment>(Messages.PostCommentNotFound);
            }
            return new SuccessDataResult<PostComment>(result);
        }

        public IResult Create(PostComment entity)
        {
            _postCommentDal.Add(entity);
            return new SuccessResult(Messages.PostCommentCreated);
        }

        public IResult Update(PostComment entity)
        {
            _postCommentDal.Update(entity);
            return new SuccessResult(Messages.PostCommentUpdated);
        }

        public IResult Delete(PostComment entity)
        {
            _postCommentDal.Delete(entity);
            return new SuccessResult(Messages.PostCommentDeleted);
        }

        public IDataResult <List<PostComment>> GetPostCommentRelatedEntitiesByPostId(int postId)
        {
            var result= _postCommentDal.GetPostsCommentRelatedEntites().Where(ps => ps.PostId == postId).ToList();
            if (result.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<PostComment>>(Messages.PostCommentsNotFound);
            }
            return new SuccessDataResult<List<PostComment>>(result);
        }

        public IDataResult<bool> PostCommentExist(int userId, int postId)
        {
            var result =_postCommentDal.GetList().Where(pc => pc.UserId == userId && pc.PostId == postId).Count() > 0;
            return new SuccessDataResult<bool>(result);
        }
    }
}
