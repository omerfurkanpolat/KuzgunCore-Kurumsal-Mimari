using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostsDTO
{
    public class PostForCreationDTO : IDto
    {
       
        public string Title { get; set; }
      
        public string Body { get; set; }

        
        public string ImageUrl { get; set; }
       
        public int SubCategoryId { get; set; }
     
        public int CategoryId { get; set; }
    }
}
