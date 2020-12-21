using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Bussines.Abstract;

using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class SubCategoryManager:ISubCategoryService
    {
        private ISubCategoryDal _subCategoryDal;

        public SubCategoryManager(ISubCategoryDal subCategoryDal)
        {
            _subCategoryDal = subCategoryDal;
        }

        public List<SubCategory> GetAll()
        {
            return _subCategoryDal.GetList();
        }

        public SubCategory GetById(int id)
        {
            return _subCategoryDal.Get(s => s.SubCategoryId == id);
        }

        public void Create(SubCategory entity)
        {
            _subCategoryDal.Add(entity);
        }

        public void Update(SubCategory entity)
        {
            _subCategoryDal.Update(entity);
        }

        public void Delete(SubCategory entity)
        {
            _subCategoryDal.Delete(entity);
        }
    }
}
