using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Core.Utilities.Security;

namespace Kuzgun.Bussines.Abstract
{
    public interface IAuthService
    {
         Task<IDataResult<AccessToken>> CreateAccessToken(User user);

         Task<List<Role>> ChangeRoleType( List<string> roleList);
    }
}
