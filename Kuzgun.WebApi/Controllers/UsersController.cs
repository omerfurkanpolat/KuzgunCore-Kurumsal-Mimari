using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.ComplexTypes.PostCommentsDTO;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IPostCommentService _postCommentService;

        public UsersController(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        //
        [HttpGet]
        [Route("commentExists/{userId}/{postId}")]
        public IActionResult CommentExists(int userId, int postId)
        {
            var exists = _postCommentService.PostCommentExist(userId, postId);  
            CommentForReturnDTO model = new CommentForReturnDTO();
            model.Exists = exists;

            return Ok(model);
        }

        [HttpGet]
        [Route("getComment/{id}")]
        public IActionResult GetComment(int id)
        {
            var postComment = _postCommentService.GetById(id);
            if (postComment == null)
                return BadRequest("Yorum bulunamadı");
            //var result = _mapper.Map<CommentForReturnDTO>(postComment);
            return Ok(postComment);
        }

        [HttpPost]
        [Route("addcomment/{postId}/{userId}")]
        public IActionResult AddComment(int postId, int userId, CommentForCreationDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var result = _postCommentService.PostCommentExist(userId, postId);  
            if (result == true)
                return BadRequest("Bir makalaye birden fazla yorum ekleyemezsiniz");

            PostComment postComment = new PostComment()
            {
                UserId = userId,
                PostId = postId,
                Comment = model.Comment,
                DateCreated = DateTime.Now
            };
            _postCommentService.Create(postComment);
            return Ok();
        }

        [HttpPut]
        [Route("updateComment/{commentId}/{userId}")]
        public IActionResult UpdateComment(int commentId, int userId, CommentForUpdateDTO model)
        {

            if (!ModelState.IsValid)
                return BadRequest("girdiğiniz bilgiler eksik yahut hatalı");

            var postComment = _postCommentService.GetById(commentId);  

            if (postComment == null)
                return BadRequest("böyle bir yorum henüz girilmemiş");


            if (postComment.UserId != userId)
                return BadRequest( "Bu yorumu güncellemeye yetkiniz yok");


            postComment.Comment = model.comment;
            _postCommentService.Update(postComment);

            return Ok();
        }

        [HttpDelete]
        [Route("deletecomment/{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            var postComment = _postCommentService.GetById(commentId);  

            if (postComment == null)
                return BadRequest("böyle bir yorum yok");

            _postCommentService.Delete(postComment);
            return Ok();
        }







    }
}
