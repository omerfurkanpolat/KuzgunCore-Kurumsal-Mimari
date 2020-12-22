using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Core.Utilities.Security;
using Microsoft.AspNetCore.Identity;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class AuthManager:IAuthService
    {
        private ITokenHelper _tokenHelper;

        public AuthManager(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }


        public IDataResult<AccessToken> CreateAccessToken(User user, List<Role> roles)
        {
            
            var accessToken = _tokenHelper.CreateToken(user,roles);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
