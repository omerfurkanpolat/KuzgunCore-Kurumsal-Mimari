using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.CategoriesDTO
{
    public class CategoryForUpdateDTO : IDto
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
