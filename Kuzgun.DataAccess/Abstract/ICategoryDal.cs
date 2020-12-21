using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.DataAccess;

using Kuzgun.Entities.Concrete;

namespace Kuzgun.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
    }
}
