using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Core.Entity.Concrete;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation
{
    public class PostValidator:AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(p => p.Body).NotEmpty();
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.ImageUrl).NotEmpty();
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.SubCategoryId).NotEmpty();
        }
    }
}
