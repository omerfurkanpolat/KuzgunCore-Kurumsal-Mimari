using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<Category> GetById(int id);
        IResult Create(Category entity);
        IResult Update(Category entity);
        IResult Delete(Category entity);
        IDataResult<List<int>> GetCategoriesId();
        IDataResult<List<Category>> GetActiveCategories();
    }
}
