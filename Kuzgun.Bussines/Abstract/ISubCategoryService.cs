using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface ISubCategoryService
    {
        List<SubCategory> GetAll();
        SubCategory GetById(int id);
        void Create(SubCategory entity);
        void Update(SubCategory entity);
        void Delete(SubCategory entity);
    }
}
