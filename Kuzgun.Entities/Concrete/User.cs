using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Kuzgun.Entities.Concrete
{
    public class User:IdentityUser<int>
    {
        

        public DateTime DateRegistered { get; set; }

        public DateTime LastActive { get; set; }

        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Post> Posts { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
    }
}
