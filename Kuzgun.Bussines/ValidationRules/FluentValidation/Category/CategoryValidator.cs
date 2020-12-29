using FluentValidation;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.Category
{
    public class CategoryValidator:AbstractValidator<Core.Entity.Concrete.Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.CategoryName).NotNull();
            RuleFor(c => c.DateCreated).NotEmpty();
            
        }
    }
}
