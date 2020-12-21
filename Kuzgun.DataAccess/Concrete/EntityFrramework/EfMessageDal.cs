using Kuzgun.Core.DataAccess.EntityFramework;
using Kuzgun.DataAccess.Abstract;
using Kuzgun.Entities.Concrete;

namespace Kuzgun.DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, KuzgunContext>, IMessageDal
    {
    }
}
