using System;
using System.Collections.Generic;

namespace Kuzgun.Core.Entity.Concrete
{
    public class SubCategory:IEntity
    {
       
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
