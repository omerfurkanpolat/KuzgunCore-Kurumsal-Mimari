using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.RolesDTO
{
    public class RoleForUpdateDTO : IDto
    {
        [Required]
        public string Name { get; set; }
    }
}
