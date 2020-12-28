using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForLoginDtoValidator:AbstractValidator<UserForLoginDTO>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(u => u.UserName).NotEmpty();
            RuleFor(u => u.UserName).NotNull();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();

        }
    }
}
