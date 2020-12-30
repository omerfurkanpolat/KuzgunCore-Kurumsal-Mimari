using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _mapper;

        public PostsController(IPostService postService, IPostCommentService postCommentService, ICategoryService categoryService, IMapper mapper)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getLastPost")]
        public IActionResult LastPost()
        {
            var post = _postService.GetLastPost();
            if (post.Success)
            {
                var result = _mapper.Map<PostForReturnDTO>(post.Data);
                return Ok(result);

            }
            return BadRequest(post.Message);

        }


        [HttpGet]
        [Route("getAllPost")]
        public IActionResult GetAllPost()
        {
            var posts = _postService.GetPostsRelatedEntities();
            if (posts.Success)
            {
                var result = _mapper.Map<List<PostForReturnDTO>>(posts.Data);
                return Ok(result);
            }

            return BadRequest(posts.Message);


        }

        [HttpGet]
        [Route("getPostsBySubCategory/{subCategoryId}")]
        public IActionResult GetPostsBySubCategory(int subCategoryId)
        {
            var posts = _postService.GetPostsBySubCategoryId(subCategoryId);
            if (posts.Success)
            {
                var result = _mapper.Map<List<PostForReturnDTO>>(posts.Data);
                return Ok(result);

            }
            return BadRequest(posts.Message);


        }


        [HttpGet]
        [Route("getPostsByCategory/{categoryId}")]
        public IActionResult GetPostsByCategory(int categoryId)
        {
            var posts = _postService.GetPostsByCategoryId(categoryId);
            if (posts.Success)
            {
                var result = _mapper.Map<List<PostForReturnDTO>>(posts.Data);
                return Ok(result);
            }

            return BadRequest(posts.Message);
        }

        [HttpGet]
        [Route("getPostPostById/{postId}")]
        public IActionResult GetPostById(int postId)
        {
            var post = _postService.GetPostRelatedEntitiesById(postId);
            if (post.Success)
            {
                var result = _mapper.Map<PostForReturnDTO>(post.Data);
                return Ok(result);
            }

            return BadRequest(post.Message);
        }

        [HttpGet]
        [Route("getPostByUser/{userId}")]
        public IActionResult GetPostByUser(int userId)
        {
            var posts = _postService.GetPostByUserId(userId);
            if (posts.Success)
            {
                var result = _mapper.Map<IEnumerable<PostForReturnDTO>>(posts.Data);
                return Ok(result);
            }

            return BadRequest(posts.Message);
        }

        [HttpGet]
        [Route("getCommentByPostId/{postId}")]
        public IActionResult GetCommentByPostId(int postId)
        {
            var comments = _postCommentService.GetPostCommentRelatedEntitiesByPostId(postId);
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

            foreach (int categoryId in categoriesId.Data)
            {
                var post = _postService.GetLastPostOfCategories(categoryId);

                if (post == null)
                {
                    continue;
                }
                posts.Add(post.Data);

            }
            var result = _mapper.Map<List<PostForReturnDTO>>(posts);

            return Ok(result);

        }

    }
}
