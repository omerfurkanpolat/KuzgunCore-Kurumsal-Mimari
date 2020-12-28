using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Bussines.ValidationRules.FluentValidation;
using Kuzgun.Bussines.ValidationRules.FluentValidation.Category;
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

        
        //[SecuredOperation("admin")]
        //[CacheAspect(duration:2)]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList()); 
        }
        [PerformanceAspect(5)]
        public IDataResult<Category> GetById(int id)
        {
            
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id));  
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Create(Category entity)
        {
            _categoryDal.Add(entity);
            return new SuccessResult(Messages.CategoryAdded); 
        }

        
        public IResult Update(Category entity)
        {
            _categoryDal.Update(entity);
            
            return new SuccessResult(Messages.CategoryUpdated);
        }

        public IResult Delete(Category entity)
        {
            _categoryDal.Delete(entity);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<int>> GetCategoriesId()
        {
            return new SuccessDataResult<List<int>>(_categoryDal.GetList().Select(c => c.CategoryId).ToList()); 
        }
    }
}
