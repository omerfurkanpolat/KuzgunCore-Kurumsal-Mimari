using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForConfirmEmailDToValidator:AbstractValidator<UserForConfirmEmailDTO>
    {
        public UserForConfirmEmailDToValidator()
        {
            RuleFor(u => u.Code).NotEmpty();
            RuleFor(u => u.Code).NotNull();
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.UserId).NotNull();
        }
    }
}
