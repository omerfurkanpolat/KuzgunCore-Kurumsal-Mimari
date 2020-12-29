using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.Bussines.Abstract
{
    public interface ISubCategoryService
    {
        IDataResult<List<SubCategory>>  GetAll();
        IDataResult<SubCategory> GetById(int id);
        IResult Create(SubCategory entity);
        IResult Update(SubCategory entity);
        IResult Delete(SubCategory entity);
        IDataResult<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
        IDataResult<List<SubCategory>> GetActiveSubCategoriesByCategoryId(int categoryId);

    }
}
