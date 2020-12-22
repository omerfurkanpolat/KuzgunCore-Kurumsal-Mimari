using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes.PostCommentsDTO;
using Kuzgun.Entities.ComplexTypes.PostsDTO;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;
        private IPostCommentService _postCommentService;
        private ICategoryService _categoryService;

        public PostsController(IPostService postService, IPostCommentService postCommentService, ICategoryService categoryService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _categoryService = categoryService;
        }

        [HttpGet]
        [Route("getLastPost")]
        public IActionResult LastPost()
        {
            var post = _postService.GetLastPost();
            if (post == null)
            { return BadRequest("böyle bir kayıt mevcut değil"); }
            //var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(post);
        }


        [HttpGet]
        [Route("getAllPost")]
        public IActionResult GetAllPost()
        {
            var posts = _postService.GetPostsRelatedEntites();
            //var result = _mapper.Map<IEnumerable<PostForReturnDTO>>(posts);
            return Ok(posts);
        }

        [HttpGet]
        [Route("getPostsBySubCategory/{subCategoryId}")]
        public IActionResult GetPostsBySubCategory(int subCategoryId)
        {

            var posts = _postService.GetPostsBySubCategoryId(subCategoryId);    
            if (posts == null)
                return BadRequest("böyle bir kayıt mevcut değil");
            //var result = _mapper.Map<List<PostForReturnDTO>>(posts);
            return Ok(posts);
        }


        [HttpGet]
        [Route("getPostsByCategory/{categoryId}")]
        public IActionResult GetPostsByCategory(int categoryId)
        {
            var posts = _postService.GetPostsByCategoryId(categoryId);

            if (posts == null)
                return BadRequest("böyle bir kayıt mevcut değil");

            //var result = _mapper.Map<List<PostForReturnDTO>>(posts);

            return Ok(posts);
        }

        [HttpGet]
        [Route("getPostPostById/{postId}")]
        public IActionResult GetPostById(int postId)
        {
            var post = _postService.GetPostRelatedEntitesById(postId); 
            if (post == null)
            { return BadRequest("böyle bir kayıt mevcut değil"); }

            //var result = _mapper.Map<PostForReturnDTO>(post);
            return Ok(post);
        }

        [HttpGet]
        [Route("getPostByUser/{userId}")]
        public IActionResult GetPostByUser(int userId)
        {
            var posts = _postService.GetPostByUserId(userId);
            //var result = _mapper.Map<IEnumerable<PostForReturnDTO>>(posts);
            return Ok(posts);
        }

        [HttpGet]
        [Route("getCommentByPostId/{postId}")]
        public IActionResult  GetCommentByPostId(int postId)
        {
            var comments = _postCommentService.GetPostCommentRelatedEntitesByPostId(postId);
            if (comments == null)
            {
                return BadRequest("Yorum Bulunamadı");
            }
            //var result = _mapper.Map<List<CommentForReturnDTO>>(comments);
            return Ok(comments);
        }

        [HttpGet]
        [Route("getCategoriesLastPost")]
        public IActionResult GetCategoryLastPost()
        {
            List<Post> posts = new List<Post>();

            var categoriesId = _categoryService.GetCategoriesId();   

            foreach (int categoryId in categoriesId.Data )
            {
                Post post = _postService.GetLastPostOfCategories(categoryId);

                if (post == null)
                {
                    continue;
                }
                posts.Add(post);

            }
            //var result = _mapper.Map<List<PostForReturnDTO>>(posts);

            return Ok(posts);

        }

    }
}
