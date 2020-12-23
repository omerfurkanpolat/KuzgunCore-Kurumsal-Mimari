using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
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
            //if (!ModelState.IsValid)
            //    return BadRequest("Eksik veya hatalı bilgi girdiniz");

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
                return BadRequest("Eksik yahut hatalı bilgi girdiniz");

            var post = _postService.GetById(model.PostId); 
            if (post == null)
                return BadRequest("böyle bir post mevcut değil");


            if (post.UserId != model.UserId && role != "admin")
            {
                return BadRequest("Bu Makaleyi Değiştirmeye Yetkiniz Yok");
            }

            post.Title = model.Title;
            post.Body = model.Body;
            post.ImageUrl = model.ImageUrl;
            post.SubCategoryId = model.SubCategoryId;
            post.CategoryId = model.CategoryId;

            _postService.Update(post);

            return Ok();

        }

        [HttpDelete]
        [Route("deletePost/{postId}")]
        public IActionResult DeletePost( int postId)
        {

            var post = _postService.GetById(postId); 

            if (post == null)
                return BadRequest("Silmeye çalıştığınız kayıt mevcut değil");

            _postService.Delete(post);
       

            return Ok();

        }









    }
}
