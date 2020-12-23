using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.ValidationRules.FluentValidation;
using Kuzgun.Core.Aspects.Autofac.Validation;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class PostManager:IPostService
    {
        private IPostDal _postDal;
        private IPostStatDal _postStatDal;

        public PostManager(IPostDal postDal)
        {
            _postDal = postDal;
        }

        public List<Post> GetAll()
        {
            return _postDal.GetList();
        }

        public Post GetById(int id)
        {
            return _postDal.Get(p => p.PostId == id);
        }
        
        [ValidationAspect(typeof(PostValidator),Priority = 1)]
        public void Create(Post entity, int subCategoryId)
        {
            _postDal.Add(entity);
            PostStat postStat = new PostStat();
            postStat.PostStatId = entity.PostId;
            postStat.Views = 0;
            postStat.Claps = 0;

            _postStatDal.Add(postStat);

        }


        public void Update(Post entity)
        {
            _postDal.Update(entity);
        }

        public void Delete(Post entity)
        {
            _postDal.Delete(entity);
        }

        public Post GetLastPost()
        {
            return _postDal.GetPostsRelatedEntites().OrderByDescending(p => p.PostId).FirstOrDefault();
        }

        public List<Post> GetPostsRelatedEntites()
        {
            return _postDal.GetPostsRelatedEntites();
        }

        public List<Post> GetPostsBySubCategoryId(int subCategoryId)
        {
            return _postDal.GetPostsRelatedEntites().Where(p=>p.SubCategoryId==subCategoryId).ToList();
        }

        public List<Post> GetPostsByCategoryId(int categoryId)
        {
            return _postDal.GetPostsRelatedEntites().Where(p => p.CategoryId == categoryId).ToList();
        }

        public Post GetPostRelatedEntitesById(int postId)
        {
            return _postDal.GetPostsRelatedEntites().Where(p => p.PostId==postId).FirstOrDefault();
        }

        public List<Post> GetPostByUserId(int userId)
        {
            return _postDal.GetPostsRelatedEntites().Where(p => p.UserId == userId).ToList();
        }

        public Post GetLastPostOfCategories(int categoryId)
        {
            return _postDal.GetPostsRelatedEntites().OrderByDescending(p => p.PostId)
                .FirstOrDefault(p => p.CategoryId == categoryId);
        }
    }
}
