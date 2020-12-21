using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForRegisterDTO : IDto
    {
        public UserForRegisterDTO()
        {
            LastActive = DateTime.Now;
        }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        public DateTime LastActive { get; set; }
    }
}
