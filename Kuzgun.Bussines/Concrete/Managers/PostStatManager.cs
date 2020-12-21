using System;
using System.Collections.Generic;
using System.Text;
using Kuzgun.Bussines.Abstract;

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

        public PostStat GetById(int id)
        {
            return _postStatDal.Get(ps => ps.PostId==id);
        }

        public void Create(PostStat entity)
        {
            _postStatDal.Add(entity);
        }
    }
}
