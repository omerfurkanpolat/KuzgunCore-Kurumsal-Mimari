using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostCommentsDTO
{
    public class CommentForCreationDTO : IDto
    {
        [Required]
        public string Comment { get; set; }
    }
}
