using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IPostService
    {
        IDataResult<List<Post>> GetAll();
        IDataResult<Post> GetById(int id);
        IResult Create(Post entity, int subCategoryId);
        IResult Update(Post entity);
        IResult Delete(Post entity);
        IDataResult<Post> GetLastPost();
        IDataResult<List<Post>> GetPostsRelatedEntities();
        IDataResult<List<Post>> GetPostsBySubCategoryId(int subCategoryId);
        IDataResult<List<Post>> GetPostsByCategoryId(int categoryId);
        IDataResult<Post> GetPostRelatedEntitiesById(int postId);
        IDataResult<List<Post>> GetPostByUserId(int userId);
        IDataResult<Post> GetLastPostOfCategories(int categoryId);
    }
}
