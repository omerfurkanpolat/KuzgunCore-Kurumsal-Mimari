using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostsDTO
{
    public class PostForReturnDTO : IDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string ImageUrl { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Views { get; set; } = 0;
        public int Claps { get; set; } = 0;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string UserImageUrl { get; set; }
    }
}
