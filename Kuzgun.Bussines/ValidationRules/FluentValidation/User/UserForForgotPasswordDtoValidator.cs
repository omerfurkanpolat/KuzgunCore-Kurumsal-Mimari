using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForForgotPasswordDtoValidator:AbstractValidator<UserForForgotPasswordDTO>
    {
        public UserForForgotPasswordDtoValidator()
        {
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}
