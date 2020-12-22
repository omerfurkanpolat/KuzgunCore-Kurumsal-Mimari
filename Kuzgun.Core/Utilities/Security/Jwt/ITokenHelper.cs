using System.Collections.Generic;
using Kuzgun.Core.Entity.Concrete;

namespace Kuzgun.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccesToken CreateToken(User user, List<Role> roles);
    }
}
