using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.SubCategory
{
     public class CategoryForCreationDTOValidator:AbstractValidator<CategoryForCreationDTO>
    {
        public CategoryForCreationDTOValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.CategoryName).NotNull();
        }
    }
}
