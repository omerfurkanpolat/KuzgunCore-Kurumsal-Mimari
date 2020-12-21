using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.PostCommentsDTO
{
    public class CommentForUpdateDTO : IDto
    {
        [Required]
        public string comment { get; set; }
    }
}
