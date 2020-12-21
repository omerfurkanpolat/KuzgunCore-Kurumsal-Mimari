using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForChangeRoleDTO : IDto
    {
        public string Name { get; set; }
    }
}
