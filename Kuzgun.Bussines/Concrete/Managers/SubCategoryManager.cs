using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Bussines.ValidationRules.FluentValidation.SubCategory;
using Kuzgun.Core.Aspects.Autofac.Authorization;
using Kuzgun.Core.Aspects.Autofac.Validation;
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
            var result = _subCategoryDal.GetList();
            if (result==null)
            {
                return new ErrorDataResult<List<SubCategory>>(Messages.SubCategoriesNotFound);
            }
            return new SuccessDataResult<List<SubCategory>>(result);
        }

        public IDataResult< SubCategory> GetById(int id)
        {
            var result = _subCategoryDal.Get(s => s.SubCategoryId == id);
            if (result==null)
            {
                return new ErrorDataResult<SubCategory>(Messages.SubCategoryNotFound);
            }
            return new SuccessDataResult<SubCategory>(result );
        }
        [SecuredOperation("user")]
        [ValidationAspect(typeof(SubCategoryValidator))]
        public IResult Create(SubCategory entity)
        {
            _subCategoryDal.Add(entity);
           return new SuccessResult(Messages.SubCategoryCreated);
        }
        [SecuredOperation("user")]
        [ValidationAspect(typeof(SubCategoryValidator))]
        public IResult Update(SubCategory entity)
        {
            _subCategoryDal.Update(entity);
            return  new SuccessResult(Messages.SubCategoryUpdated);
        }
        [SecuredOperation("user")]
        [ValidationAspect(typeof(SubCategoryValidator))]
        public IResult Delete(SubCategory entity)
        {
            _subCategoryDal.Delete(entity);
            return new SuccessResult(Messages.SubCategoryDeleted);
        }

        public IDataResult<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            var result = _subCategoryDal.GetList(s => s.CategoryId == categoryId);
            if (result==null)
            {
                return new ErrorDataResult<List<SubCategory>>(Messages.SubCategoriesNotFound);
            }
            return new SuccessDataResult<List<SubCategory>>(result);
        }

        public IDataResult<List<SubCategory>> GetActiveSubCategoriesByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<SubCategory>>(_subCategoryDal.GetList(s=>s.IsDeleted==false && s.CategoryId==categoryId));
        }
    }
}
