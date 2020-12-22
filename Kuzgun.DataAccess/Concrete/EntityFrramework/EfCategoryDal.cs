﻿
using Kuzgun.Core.DataAccess.EntityFramework;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal: EfEntityRepositoryBase<Category, KuzgunContext>, ICategoryDal
    {
    }
}
