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
    public class PostStatManager:IPostStatService
    {
        private IPostStatDal _postStatDal;

        public PostStatManager(IPostStatDal postStatDal)
        {
            _postStatDal = postStatDal;
        }

        public IDataResult<PostStat> GetById(int id)
        {
            var result= _postStatDal.Get(ps => ps.PostId == id);
            if (result==null)   
            {
                return new ErrorDataResult<PostStat>(Messages.PostStatNotFound);
            }
            return new SuccessDataResult<PostStat>(result);
        }

        public IResult Create(PostStat entity)
        {
            _postStatDal.Add(entity);
            return  new SuccessResult();
        }
    }
}
