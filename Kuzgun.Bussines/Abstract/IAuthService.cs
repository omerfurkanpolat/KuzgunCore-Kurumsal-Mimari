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
         List<Role> ChangeRoleType( List<string> roleList);
         Task<IResult> CreateUserAsync( UserForRegisterDTO userForRegisterDto);
         Task<IResult> CheckIfUserNameExistsAsync(string userName);
         Task <IResult> CheckIfEmailExistsAsync(string email);
         Task<IResult> CreateRoleAsync(string roleName);
         Task<IResult> ConfirmEmailAsync(UserForConfirmEmailDTO userForConfirmEmailDto);
         Task<IDataResult<User>> FindUserByUserIdAsync(int id);
         Task<IDataResult<User>> LoginAsync(UserForLoginDTO userForLoginDto);
         Task<IDataResult<User>> FindUserByUserNameAsync(string userName);
         IDataResult<User> IsUserDeleted(User user);
         IDataResult<User> IsEmailConfirmed(User user);
         Task<IResult> ForgotPasswordAsync(UserForForgotPasswordDTO userForForgotPasswordDto);
         Task<IDataResult<User>> FindUserByEmailAsync(string email);
         Task<IResult> ResetPasswordAsync(UserForResetPasswordDTO userForResetPasswordDto);
         Task<IResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDto, int id);
         Task<IDataResult<User>> UpdateUserAsync(User user);
         Task<IResult> ChangeEmailAddressAsync(UserForChangeEmailDTO userForChangeEmailDto, int id);
         Task<IResult> ChangeProfilePictureAsync(UserForChangeProfilePictureDTO changeProfilePictureDto, int id);
         IDataResult<List<Role>> GetRoles();
         Task<IDataResult<Role>> FindRoleByIdAsync(int roleId);
         Task<IResult> UpdateRoleAsync(Role role);
         Task<IResult> DeleteRoleAsync(Role role);
         Task<IResult> ChangeUserRoleAsync(User user, string userRole);
         Task<IResult> AddToRoleAsync(User user, string roleName);
         Task<IDataResult<List<string>>> GetUserRolesAsync(User user);
         Task<IResult> RemoveUserRoleAsync(User user, List<string> userRoles);
         Task<IDataResult<List<User>>> GetUsersAsync();
         Task<IDataResult<string>> GetUserRoleAsync(User user);
         Task<IDataResult<User>> DeleteUserAsync(int id);
         Task<IDataResult<User>> ReviveUserAsync(int id);

    }
}
