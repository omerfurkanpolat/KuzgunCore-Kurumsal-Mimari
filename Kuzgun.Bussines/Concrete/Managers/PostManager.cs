using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Internal;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Bussines.ValidationRules.FluentValidation;
using Kuzgun.Bussines.ValidationRules.FluentValidation.Post;
using Kuzgun.Core.Aspects.Autofac.Transaction;
using Kuzgun.Core.Aspects.Autofac.Validation;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class PostManager:IPostService
    {
        private IPostDal _postDal;
        private IPostStatService _postStatService;

        public PostManager(IPostDal postDal, IPostStatService postStatService)
        {
            _postDal = postDal;
            _postStatService = postStatService;
        }

        public IDataResult<List<Post>>  GetAll()
        {
            var result = _postDal.GetList();
            if (result==null)
            {
                return new ErrorDataResult<List<Post>>(Messages.PostsNotFound);
            }
            return new SuccessDataResult<List<Post>>(result);
        }

        public IDataResult <Post> GetById(int id)
        {
            var result = _postDal.Get(p => p.PostId == id);
            if (result==null)
            {
                return new ErrorDataResult<Post>(Messages.PostNotFound);
            }
            return  new SuccessDataResult<Post>(result);
        }
        

        [ValidationAspect(typeof(PostValidator),Priority = 1)]
        [TransactionScopeAspect]
        public IResult Create(Post entity, int subCategoryId)
        {
            _postDal.Add(entity);
            PostStat postStat = new PostStat();
            postStat.PostStatId = entity.PostId;
            postStat.Views = 0;
            postStat.Claps = 0;

            _postStatService.Create(postStat);
            return new SuccessResult();

        }


        public IResult Update(Post entity)
        {
            
            _postDal.Update(entity);
            return new SuccessResult(Messages.PostUpdated);
        }

        public IResult Delete(Post entity)
        {
            _postDal.Delete(entity);
            return new SuccessResult(Messages.PostDeleted);
        }

        public IDataResult <Post> GetLastPost()
        {
            var result= _postDal.GetPostsRelatedEntites().OrderByDescending(p => p.PostId).FirstOrDefault();
            if (result==null)
            {
                return new ErrorDataResult<Post>(Messages.LastPostNotFound);
            }
            return  new SuccessDataResult<Post>(result);
        }

        public IDataResult <List<Post>> GetPostsRelatedEntities()
        {
            var result= _postDal.GetPostsRelatedEntites();
            if (result==null)
            {
                return new ErrorDataResult<List<Post>>(Messages.PostsNotFound);
            }
            return  new SuccessDataResult<List<Post>>(result);
        }

        public IDataResult< List<Post>> GetPostsBySubCategoryId(int subCategoryId)
        {
            var result= _postDal.GetPostsRelatedEntites().Where(p => p.SubCategoryId == subCategoryId).ToList();
            if (result.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<Post>>(Messages.PostsNotFound);
            }
            return new SuccessDataResult<List<Post>>(result);
        }

        public IDataResult <List<Post>> GetPostsByCategoryId(int categoryId)
        {
            var result= _postDal.GetPostsRelatedEntites().Where(p => p.CategoryId == categoryId).ToList();
            if (result.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<Post>>(Messages.PostsNotFound);
            }
            return new SuccessDataResult<List<Post>>(result);
        }

        public IDataResult <Post> GetPostRelatedEntitiesById(int postId)
        {
            var result = _postDal.GetPostsRelatedEntites().Where(p => p.PostId == postId).FirstOrDefault(); 
            if (result==null)
            {
              return  new ErrorDataResult<Post>(Messages.PostNotFound);
            }
            return  new SuccessDataResult<Post>(result);
        }

        public IDataResult<List<Post>>  GetPostByUserId(int userId)
        {
            var result= _postDal.GetPostsRelatedEntites().Where(p => p.UserId == userId).ToList();
            if (result.IsNullOrEmpty())
            {
                
                return new ErrorDataResult<List<Post>>(Messages.PostsNotFound);
            }
            return new SuccessDataResult<List<Post>>(result);
        }

        public IDataResult<Post> GetLastPostOfCategories(int categoryId)
        {
            var result= _postDal.GetPostsRelatedEntites().OrderByDescending(p => p.PostId)
                .FirstOrDefault(p => p.CategoryId == categoryId);
            if (result==null)
            {
                return new ErrorDataResult<Post>(Messages.PostsNotFound);
            }
            return new SuccessDataResult<Post>(result);
        }
    }
}
