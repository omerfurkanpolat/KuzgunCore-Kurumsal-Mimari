using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForResetPasswordDtoValidator:AbstractValidator<UserForResetPasswordDTO>
    {
        public UserForResetPasswordDtoValidator()
        {
            RuleFor(u => u.Code).NotEmpty();
            RuleFor(u => u.Code).NotNull();
            RuleFor(u => u.UserId).NotEmpty();
            RuleFor(u => u.UserId).NotNull();
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).NotNull();
            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u.ConfirmPassword).NotNull();
            RuleFor(u => u.ConfirmPassword).Must(CompareWithPassword);
        }

        private bool CompareWithPassword(UserForResetPasswordDTO arg1, string arg2)
        {
            if (arg1.Password==arg1.ConfirmPassword)
            {
                return true;
            }

            return false;
        }
    }
}
