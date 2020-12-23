using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;
using Kuzgun.Core.Entity.Concrete;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.DateCreated).NotEmpty();
            
        }
    }
}
