using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForLoginDTO : IDto
    {

        [Required(ErrorMessage = "Kulllanıcı adı alanı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakalamaz")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
