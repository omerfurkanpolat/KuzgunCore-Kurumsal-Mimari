using System;
using System.Collections.Generic;
using System.Text;

using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface IPostStatService
    {
        PostStat GetById(int id);
        void Create(PostStat entity);
    }
}
