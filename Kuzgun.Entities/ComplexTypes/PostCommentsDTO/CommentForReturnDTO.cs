using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostCommentsDTO
{
    public class CommentForReturnDTO : IDto
    {
        public int PostCommentId { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public string Comment { get; set; }
        public string UserName { get; set; }
        public bool Exists { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
