using System;

namespace Kuzgun.Core.Entity.Concrete
{
    public class PostComment:IEntity
    {
        public int PostCommentId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
