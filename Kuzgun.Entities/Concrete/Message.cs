using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.Concrete
{
    public class Message:IEntity
    {
        public int MessageId { get; set; }

        public string MessageHeader { get; set; }

        public string MessageBody { get; set; }

        
        public string Email { get; set; }

        public string FullName { get; set; }
        public string Answer { get; set; }
    }
}
