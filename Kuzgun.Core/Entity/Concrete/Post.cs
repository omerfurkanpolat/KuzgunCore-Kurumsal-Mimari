using System;
using System.Collections.Generic;

namespace Kuzgun.Core.Entity.Concrete
{
    public class Post:IEntity
    {
       
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; } = true;
        public virtual PostStat PostStat { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PostComment> PostComments { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
