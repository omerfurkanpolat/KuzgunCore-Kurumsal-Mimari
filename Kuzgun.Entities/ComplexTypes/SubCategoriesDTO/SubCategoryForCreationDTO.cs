using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.SubCategoriesDTO
{
    public class SubCategoryForCreationDTO : IDto
    {
        [Required]
        public string SubCategoryName { get; set; }
        

    }
}
