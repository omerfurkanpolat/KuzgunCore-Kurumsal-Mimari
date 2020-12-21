using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForChangeProfilePictureDTO : IDto
    {
        [Required]
        public string ImageUrl { get; set; }
    }
}
