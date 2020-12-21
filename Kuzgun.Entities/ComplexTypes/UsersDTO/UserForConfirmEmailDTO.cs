using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForConfirmEmailDTO : IDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
