using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
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

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList()); 
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == id));  
        }

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
