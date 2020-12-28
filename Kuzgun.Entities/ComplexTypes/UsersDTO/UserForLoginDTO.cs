using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForLoginDTO : IDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
