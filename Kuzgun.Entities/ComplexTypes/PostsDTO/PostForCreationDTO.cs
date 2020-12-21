using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostsDTO
{
    public class PostForCreationDTO : IDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
