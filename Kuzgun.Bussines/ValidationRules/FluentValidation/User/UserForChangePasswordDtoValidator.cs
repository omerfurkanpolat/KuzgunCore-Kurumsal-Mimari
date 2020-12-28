using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForChangePasswordDtoValidator:AbstractValidator<UserForChangePasswordDTO>
    {
        public UserForChangePasswordDtoValidator()
        {
            RuleFor(u => u.OldPassword).NotEmpty();
            RuleFor(u => u.OldPassword).NotNull();
            RuleFor(u => u.NewPassword).NotEmpty();
            RuleFor(u => u.NewPassword).NotNull();
            RuleFor(u => u.ConfirmPassword).NotEmpty();
            RuleFor(u => u.ConfirmPassword).NotNull();
            RuleFor(u => u.ConfirmPassword).Must(CompareWithPassword);

        }

        private bool CompareWithPassword(UserForChangePasswordDTO arg1, string arg2)
        {
            if (arg1.NewPassword==arg1.ConfirmPassword)
            {
                return true;
            }

            return false;
        }
    }
}
