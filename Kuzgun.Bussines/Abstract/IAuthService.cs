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
         IDataResult<AccessToken> CreateAccessToken(User user, List<Role> roles);
    }
}
