using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
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
        private IMapper _mapper;

        public UsersController(IPostCommentService postCommentService, IMapper mapper)
        {
            _postCommentService = postCommentService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("commentExists/{userId}/{postId}")]
        public IActionResult CommentExists(int userId, int postId)
        {
            var exists = _postCommentService.PostCommentExist(userId, postId);

            CommentForReturnDTO model = new CommentForReturnDTO();
            model.Exists = exists.Data;

            return Ok(model);
        }

        [HttpGet]
        [Route("getComment/{id}")]
        public IActionResult GetComment(int id)
        {
            var postComment = _postCommentService.GetById(id);
            if (postComment.Success)
            {
                var result = _mapper.Map<CommentForReturnDTO>(postComment);
                return Ok(result);
            }

            return BadRequest(postComment.Message);


        }

        [HttpPost]
        [Route("addcomment/{postId}/{userId}")]
        public IActionResult AddComment(int postId, int userId, CommentForCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Messages.ModelNullOrEmpty);
            }
            var result = _postCommentService.PostCommentExist(userId, postId);
            if (result.Data == true)
            {
                return BadRequest(Messages.OneCommentToOnePost);
            }
 
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
            {
                return BadRequest(Messages.ModelNullOrEmpty);

            }
            var postComment = _postCommentService.GetById(commentId);

            if (!postComment.Success)
            {
                return BadRequest(postComment.Message);
            }

            if (postComment.Data.UserId != userId)
            {
                return BadRequest(Messages.YouUnauthorizeChangeThisComment);
            }
                
            postComment.Data.Comment = model.comment;
            var result= _postCommentService.Update(postComment.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }

        [HttpDelete]
        [Route("deletecomment/{commentId}")]
        public IActionResult DeleteComment(int commentId)
        {
            var postComment = _postCommentService.GetById(commentId);

            if (!postComment.Success)
            {
                return BadRequest(postComment.Message);
            }

            var result=_postCommentService.Delete(postComment.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }







    }
}
