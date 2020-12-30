using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;
using Kuzgun.Core.Entity.Concrete;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.SubCategory
{
    public class SubCategoryValidator:AbstractValidator<Core.Entity.Concrete.SubCategory>
    {
        public SubCategoryValidator()
        {
            RuleFor(s => s.SubCategoryName).NotEmpty();
            RuleFor(s => s.SubCategoryName).NotNull();
            RuleFor(s => s.CategoryId).NotEmpty();
            RuleFor(s => s.CategoryId).NotNull();
            RuleFor(s => s.IsDeleted).NotEmpty();
            RuleFor(s => s.IsDeleted).NotNull();
            
        }
    }
}
