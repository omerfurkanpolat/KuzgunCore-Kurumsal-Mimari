using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Core.Utilities.Security;
using Kuzgun.Entities.ComplexTypes.UsersDTO;

namespace Kuzgun.Bussines.Abstract
{
    public interface IAuthService
    {
         Task<IDataResult<AccessToken>> CreateAccessToken(User user);
         Task<List<Role>> ChangeRoleType( List<string> roleList);
         Task<IResult> CreateUser( UserForRegisterDTO userForRegisterDto);
         Task<IResult> CheckIfUserNameExists(string userName);
         Task <IResult> CheckIfEmailExists(string email);
         Task<IResult> CreateRole(string roleName);
         Task<IResult> ConfirmEmail(UserForConfirmEmailDTO userForConfirmEmailDto);
         Task<IDataResult<User>> CheckByUserId(int id);
         Task<IDataResult<User>> Login(User user,UserForLoginDTO userForLoginDto);
         Task<IDataResult<User>> GetUserByUserName(string userName);
         IResult UserIsDeleted(User user);
         IResult IsEmailConfirmed(User user);
         Task<IResult> ForgotPassword(UserForForgotPasswordDTO userForForgotPasswordDto);
         Task<IDataResult<User>> GetUserByEmail(string email);

    }
}
