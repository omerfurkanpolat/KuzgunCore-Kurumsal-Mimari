using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.ValidationRules.FluentValidation.User
{
    public class UserForChangeProfilePictureDtoValidator:AbstractValidator<UserForChangeProfilePictureDTO>
    {
        public UserForChangeProfilePictureDtoValidator()
        {
            RuleFor(u => u.ImageUrl).NotEmpty();
            RuleFor(u => u.ImageUrl).NotNull();
        }
    }
}
