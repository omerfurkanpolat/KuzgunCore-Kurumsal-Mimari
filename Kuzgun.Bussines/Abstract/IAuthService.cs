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
         Task<IDataResult<User>> FindByUserId(int id);
         Task<IResult> Login(UserForLoginDTO userForLoginDto);
         Task<IDataResult<User>> FindByUserName(string userName);
         IResult UserIsDeleted(User user);
         IResult IsEmailConfirmed(User user);
         Task<IResult> ForgotPassword(UserForForgotPasswordDTO userForForgotPasswordDto);
         Task<IDataResult<User>> FindByEmail(string email);
         Task<IResult> ResetPassword(UserForResetPasswordDTO userForResetPasswordDto);
         Task<IResult> ChangePassword(UserForChangePasswordDTO userForChangePasswordDto, int id);
         Task<IDataResult<User>> UpdateUser(User user);
         Task<IResult> ChangeEmailAddress(UserForChangeEmailDTO userForChangeEmailDto, int id);
         Task<IResult> ChangeProfilePicture(UserForChangeProfilePictureDTO changeProfilePictureDto, int id);

    }
}
