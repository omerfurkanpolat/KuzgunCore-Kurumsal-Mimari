using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForResetPasswordDTO : IDto
    {
        
        public string Password { get; set; }

     
        public string ConfirmPassword { get; set; }
        
        public int UserId { get; set; }
      
        public string Code { get; set; }
    }
}
