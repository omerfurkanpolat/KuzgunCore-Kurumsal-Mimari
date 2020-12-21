using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.RolesDTO
{
    public class RoleForCreationDTO : IDto
    {
        [Required]
        [Display(Name = "Rol İsmi")]
        public string Name { get; set; }
    }
}
