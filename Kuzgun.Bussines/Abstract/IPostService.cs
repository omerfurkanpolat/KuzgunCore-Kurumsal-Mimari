using System;
using System.Collections.Generic;
using System.Text;

using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetById(int id);
        void Create(Post entity, int subCategoryId);
        void Update(Post entity);
        void Delete(Post entity);
        Post GetLastPost();
        List<Post> GetPostsRelatedEntites();
        List<Post> GetPostsBySubCategoryId(int subCategoryId);
        List<Post> GetPostsByCategoryId(int categoryId);
        Post GetPostRelatedEntitesById(int postId);
        List<Post> GetPostByUserId(int userId);
        Post GetLastPostOfCategories(int categoryId);
    }
}
