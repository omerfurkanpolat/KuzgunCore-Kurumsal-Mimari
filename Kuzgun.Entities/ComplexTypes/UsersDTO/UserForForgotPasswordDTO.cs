using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForForgotPasswordDTO : IDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
