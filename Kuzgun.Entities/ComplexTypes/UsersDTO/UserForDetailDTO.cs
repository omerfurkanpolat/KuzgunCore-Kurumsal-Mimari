using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.ComplexTypes.UsersDTO
{
    public class UserForDetailDTO : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime LastActive { get; set; }
        public string UserRole { get; set; }
    }
}
 