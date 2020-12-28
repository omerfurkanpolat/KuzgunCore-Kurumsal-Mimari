using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForRegisterDtoValidator:AbstractValidator<UserForRegisterDTO>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).NotNull();
            RuleFor(u => u.UserName ).NotEmpty();
            RuleFor(u => u.UserName ).NotNull();
            RuleFor(u => u.Password ).NotEmpty();
            RuleFor(u => u.Password ).NotNull();
        }
    }
}
