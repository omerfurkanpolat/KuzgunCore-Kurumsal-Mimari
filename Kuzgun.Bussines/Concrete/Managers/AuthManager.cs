using System;
using System.Collections.Generic;
using System.Linq;
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
        private UserManager<User> _userManager;

        public AuthManager(ITokenHelper tokenHelper, UserManager<User> userManager)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
        }


        public async Task<IDataResult<AccessToken>>CreateAccessToken(User user)
        {
            var roleList = await _userManager.GetRolesAsync(user);
            var roles=await ChangeRoleType(roleList.ToList());
            
            var accessToken = _tokenHelper.CreateToken(user,roles);
            if (accessToken==null)
            {
                return new ErrorDataResult<AccessToken>(Messages.AccessTokenNotCreated);
            }
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<List<Role>> ChangeRoleType(List<string> roleList)
        {
            var roles=new List<Role>();
            var role= new Role();
            foreach (var rol in roleList)
            {
                role.Name = rol;
                roles.Add(role);
            }

            return roles;
        }
    }
}
