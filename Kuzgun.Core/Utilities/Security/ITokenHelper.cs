using System.Collections.Generic;
using Kuzgun.Core.Entity.Concrete;

namespace Kuzgun.Core.Utilities.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<Role> roles);
    }
}
