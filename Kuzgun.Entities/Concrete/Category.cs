using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.Concrete
{
    public class Category:IEntity
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
