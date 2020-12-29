using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
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

        public IDataResult<List<SubCategory>> GetAll()
        {
            
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetList());
        }

        public IDataResult< SubCategory> GetById(int id)
        {
            return new SuccessDataResult<SubCategory>( _subCategoryDal.Get(s => s.SubCategoryId == id));
        }

        public IResult Create(SubCategory entity)
        {
            _subCategoryDal.Add(entity);
           return new SuccessResult(Messages.SubCategoryCreated);
        }

        public IResult Update(SubCategory entity)
        {
            _subCategoryDal.Update(entity);
            return  new SuccessResult(Messages.SubCategoryUpdated);
        }

        public IResult Delete(SubCategory entity)
        {
            _subCategoryDal.Delete(entity);
            return new SuccessResult(Messages.SubCategoryDeleted);
        }

        public IDataResult<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetList(s=>s.CategoryId==categoryId));
        }

        public IDataResult<List<SubCategory>> GetActiveSubCategoriesByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetList(s=>s.IsDeleted==false && s.CategoryId==categoryId));
        }
    }
}
