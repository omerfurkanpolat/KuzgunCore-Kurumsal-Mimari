using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForConfirmEmailDTO : IDto
    {
       
        public int UserId { get; set; }
    
        public string Code { get; set; }
    }
}
