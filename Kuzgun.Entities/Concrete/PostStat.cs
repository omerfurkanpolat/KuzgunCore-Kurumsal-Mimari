using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity;

namespace Kuzgun.Entities.Concrete
{
    public class PostStat:IEntity
    {
        public int PostStatId { get; set; }
        public int Views { get; set; }
        public int Claps { get; set; }
        public DateTime DateAdded { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
