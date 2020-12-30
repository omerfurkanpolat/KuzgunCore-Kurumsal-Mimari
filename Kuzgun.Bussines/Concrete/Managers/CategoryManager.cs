using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Castle.Core.Internal;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Bussines.ValidationRules.FluentValidation;
using Kuzgun.Bussines.ValidationRules.FluentValidation.Category;
using Kuzgun.Bussines.ValidationRules.FluentValidation.SubCategory;
using Kuzgun.Core.Aspects.Autofac.Authorization;
using Kuzgun.Core.Aspects.Autofac.Caching;
using Kuzgun.Core.Aspects.Autofac.Exception;
using Kuzgun.Core.Aspects.Autofac.Logging;
using Kuzgun.Core.Aspects.Autofac.Performance;
using Kuzgun.Core.Aspects.Autofac.Transaction;
using Kuzgun.Core.Aspects.Autofac.Validation;
using Kuzgun.Core.CrossCuttingConcerns.Logging.Log4net.Loggers;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.ComplexTypes.CategoriesDTO;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;


        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }


       
        //[CacheAspect(duration:2)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryDal.GetList();
            if (result==null)
            {
                return new SuccessDataResult<List<Category>>(result);
            }
             return new ErrorDataResult<List<Category>>(Messages.CategoriesNotFound);
        }
        [PerformanceAspect(5)]
        
        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryDal.Get(c => c.CategoryId == id);
            if (result == null)
            {
                return new ErrorDataResult<Category>(Messages.CategoryNotFound);
            }

            return new SuccessDataResult<Category>(result);  
        }

        //[SecuredOperation("user")]
        //[ValidationAspect(typeof(CategoryValidator))]
        //[CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryForCreationDTOValidator))]
        public IResult Create(Category entity)
        {
            
            _categoryDal.Add(entity);
           
            return new SuccessResult(Messages.CategoryAdded); 
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category entity)
        {
            _categoryDal.Update(entity);
            
            return new SuccessResult(Messages.CategoryUpdated);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Delete(Category entity)
        {
            _categoryDal.Delete(entity);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<int>> GetCategoriesId()
        {
            var result = _categoryDal.GetList().Select(c => c.CategoryId).ToList();
            if (!result.IsNullOrEmpty())
            {
                return new ErrorDataResult<List<int>>();
            }
            return new SuccessDataResult<List<int>>(); 
        }

        public IDataResult<List<Category>> GetActiveCategories()
        {
            var result = _categoryDal.GetList(c => c.IsDeleted == false);
            if (result==null)
            {
                return new SuccessDataResult<List<Category>>(result);
            }
            return new ErrorDataResult<List<Category>>(Messages.CategoriesNotFound);

           
        }
    }
}
