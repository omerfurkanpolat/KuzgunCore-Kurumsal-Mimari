using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes.PostsDTO;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        private IPostService _postService;

        public WritersController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("addPost/{userId}")]
        public IActionResult AddPost(int userId, PostForCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }

            Post post = new Post
            {
                Body = model.Body,
                Title = model.Title,
                UserId = userId,
                ImageUrl = model.ImageUrl,
                DateCreated = DateTime.UtcNow,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId

            };

            _postService.Create(post, model.SubCategoryId);

            return Ok();
        }


        [HttpPut]

        [Route("updatePost/{role}")]
        public IActionResult UpdatePost(string role, PostForUpdateDTO model)
        {

            if (!ModelState.IsValid || model == null)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
           

            var post = _postService.GetById(model.PostId);
            if (!post.Success)
            {
                return BadRequest(post.Message);
            }

            if (post.Data.UserId != model.UserId && role != "admin")
            {
                return BadRequest(Messages.YouUnauthorizeChangeThisComment);
            }

            post.Data.Title = model.Title;
            post.Data.Body = model.Body;
            post.Data.ImageUrl = model.ImageUrl;
            post.Data.SubCategoryId = model.SubCategoryId;
            post.Data.CategoryId = model.CategoryId;

            _postService.Update(post.Data);

            return Ok();

        }

        [HttpDelete]
        [Route("deletePost/{postId}")]
        public IActionResult DeletePost( int postId)
        {

            var post = _postService.GetById(postId);

            if (!post.Success)
            {
                return BadRequest(post.Message);

            }
            var result= _postService.Delete(post.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);




        }




    }
}
