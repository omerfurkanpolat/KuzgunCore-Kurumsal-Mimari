using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.Constant;
using Kuzgun.Bussines.ValidationRules.FluentValidation.User;
using Kuzgun.Core.Aspects.Autofac.Transaction;
using Kuzgun.Core.Aspects.Autofac.Validation;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.Results;
using Kuzgun.Core.Utilities.Security;
using Kuzgun.Entities.ComplexTypes.UsersDTO;
using Microsoft.AspNetCore.Identity;
using Kuzgun.Core.Utilities.Business;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Kuzgun.Bussines.Concrete.Managers
{
    public class AuthManager : IAuthService
    {
        private ITokenHelper _tokenHelper;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private IEmailService _emailService;
        private SignInManager<User> _signInManager;

        public AuthManager(ITokenHelper tokenHelper, UserManager<User> userManager, RoleManager<Role> roleManager, IEmailService emailService, SignInManager<User> signInManager)
        {
            _tokenHelper = tokenHelper;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }


        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            var roleList = await _userManager.GetRolesAsync(user);
            var roles = await ChangeRoleTypeAsync( roleList.ToList());

            var accessToken = _tokenHelper.CreateToken(user, roles);
            if (accessToken == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.AccessTokenNotCreated);
            }
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<List<Role>> ChangeRoleTypeAsync(List<string> roleList)
        {
            var roles = new List<Role>();
            var role = new Role();
            foreach (var rol in roleList)
            {
                role.Name = rol;
                roles.Add(role);
            }

            return roles;
        }
        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        [TransactionScopeAspect]
        public async Task<IResult> CreateUserAsync(UserForRegisterDTO userForRegisterDto)
        {
            var roleName = "user";

            IResult result = BusinessRules.Run(await CheckIfUserNameExistsAsync(userForRegisterDto.UserName), await CheckIfEmailExistsAsync(userForRegisterDto.Email),
                await CreateRoleAsync(roleName));

            if (result != null)
            {
                return result;
            }
            User user = new User
            {
                UserName = userForRegisterDto.UserName,
                Email = userForRegisterDto.Email,
                IsDeleted = false

            };
            await _userManager.CreateAsync(user, userForRegisterDto.Password);
            await _userManager.AddToRoleAsync(user, roleName);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = "http://localhost:4200/confirmEmail?";
            var callbackUrl = $"{url}" + $"UserId={user.Id}&" + $"Code={code}";
            _emailService.SendMail(user.UserName, user.Email, "Hesap onaylama maili",
              $"Hesabınızı onaylamak için lütfen linkle tıklayın: {callbackUrl} ");
            return new SuccessResult(Messages.ConfirmYourEmailAdress);

        }

        public async Task<IResult> CheckIfUserNameExistsAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result != null)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }
            return new SuccessResult();

        }

        public async Task<IResult> CheckIfEmailExistsAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result != null)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);

            }
            return new SuccessResult();


        }

        public async Task<IResult> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.RoleExistsAsync(roleName);

            if (result == true)
            {
                return new SuccessResult();
            }
            Role newRole = new Role
            {
                Name = roleName
            };
            await _roleManager.CreateAsync(newRole);
            return new SuccessResult(Messages.RoleCreated);

        }
        [ValidationAspect(typeof(UserForConfirmEmailDToValidator))]
        public async Task<IResult> ConfirmEmailAsync(UserForConfirmEmailDTO userForConfirmEmailDto)
        {
            IDataResult<User> result = BusinessDataRules.Run(await FindUserByUserIdAsync(userForConfirmEmailDto.UserId));
            var code = userForConfirmEmailDto.Code.Replace(" ", "+");

            if (result != null)
            {
                return result;
            }
            var result2 = await _userManager.ConfirmEmailAsync(result.Data, userForConfirmEmailDto.Code);

            if (result2.Succeeded)
            {
                return new SuccessResult(Messages.EmailConfirmed);
            }
            return new ErrorResult(Messages.EmailNotConfirmed);

        }

        public async Task<IDataResult<User>> FindUserByUserIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(user);
        }

        [ValidationAspect(typeof(UserForLoginDtoValidator))]
        public async Task<IDataResult<User>> LoginAsync(UserForLoginDTO userForLoginDto)
        {
            var result = await FindUserByUserNameAsync(userForLoginDto.UserName);

            if (!result.Success)
            {
                return result;
            }

            var checkUserDeleted = IsUserDeleted(result.Data);
            if (!checkUserDeleted.Success)
            {
                return checkUserDeleted;
            }
            var isEmailConfirmed = IsEmailConfirmed(result.Data);
            if (!isEmailConfirmed.Success)
            {
                return isEmailConfirmed;
            }

            var singInAccount = await _signInManager.CheckPasswordSignInAsync(result.Data, userForLoginDto.Password, false);
            if (!singInAccount.Succeeded)
            {
                return new ErrorDataResult<User>(Messages.UserNameOrPasswordWrong);
            }
            result.Data.LastActive = DateTime.Now;
            await UpdateUserAsync(result.Data);

            return new SuccessDataResult<User>(result.Data);

        }

        public async Task<IDataResult<User>> FindUserByUserNameAsync(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);
        }

        public IDataResult<User> IsUserDeleted(User user)
        {
            if (user.IsDeleted == true)
            {
                return new ErrorDataResult<User>(Messages.UserIsDeleted);
            }
            return new SuccessDataResult<User>();
        }

        public IDataResult<User> IsEmailConfirmed(User user)
        {
            if (user.EmailConfirmed == true)
            {
                return new SuccessDataResult<User>();
            }
            return new ErrorDataResult<User>(Messages.EmailNotConfirmed);
        }

        [ValidationAspect(typeof(UserForForgotPasswordDtoValidator))]
        public async Task<IResult> ForgotPasswordAsync(UserForForgotPasswordDTO userForForgotPasswordDto)
        {
            var user = await FindUserByEmailAsync(userForForgotPasswordDto.Email);
            if (!user.Success)
            {
                return user;
            }

            IResult result = BusinessRules.Run(IsEmailConfirmed(user.Data));
            if (result != null)
            {
                return result;
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Data);
            var url = "http://localhost:4200/resetpassword?";
            var callbackUrl = $"{url}" + $"UserId={user.Data.Id}&" + $"Code={code}";
            _emailService.SendMail(user.Data.UserName, user.Data.Email, "Parola Sıfırlama Maili",
                $"Parolanızı sıfırlamak için lütfen linkle tıklayın {callbackUrl} ");
            return new SuccessResult(Messages.SendToEmailForResetPassword);

        }

        public async Task<IDataResult<User>> FindUserByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);
        }

        [ValidationAspect(typeof(UserForResetPasswordDTO))]
        public async Task<IResult> ResetPasswordAsync(UserForResetPasswordDTO userForResetPasswordDto)
        {
            IDataResult<User> user = await FindUserByUserIdAsync(userForResetPasswordDto.UserId);
            if (!user.Success)
            {
                return user;
            }
            var revisedCode = userForResetPasswordDto.Code.Replace(" ", "+");
            var result = await _userManager.ResetPasswordAsync(user.Data, revisedCode, userForResetPasswordDto.Password);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.PasswordChanged);
            }
            return new ErrorResult(Messages.Error);

        }

        public async Task<IResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDto, int id)
        {
            IDataResult<User> user = await FindUserByUserIdAsync(id);
            if (!user.Success)
            {
                return user;
            }

            var result = await _userManager.ChangePasswordAsync(user.Data, userForChangePasswordDto.OldPassword,
                userForChangePasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.PasswordChanged);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<User>> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessDataResult<User>();
            }
            return new ErrorDataResult<User>(Messages.UserNotUpdate);
        }

        [ValidationAspect(typeof(UserForChangeEmailDtoValidator))]
        public async Task<IResult> ChangeEmailAddressAsync(UserForChangeEmailDTO userForChangeEmailDto, int id)
        {
            var user = await FindUserByUserIdAsync(id);
            if (!user.Success)
            {
                return user;
            }

            user.Data.Email = userForChangeEmailDto.Email;
            var result = await UpdateUserAsync(user.Data);
            if (result.Success)
            {
                return new SuccessResult(Messages.EmailChanged);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IResult> ChangeProfilePictureAsync(UserForChangeProfilePictureDTO changeProfilePictureDto, int id)
        {
            var user = await FindUserByUserIdAsync(id);
            if (!user.Success)
            {
                return user;
            }
            user.Data.ImageUrl = changeProfilePictureDto.ImageUrl;
            var result = await UpdateUserAsync(user.Data);
            if (result.Success)
            {
                return new SuccessResult(Messages.ProfilePictureChanged);
            }
            return new ErrorResult(Messages.Error);
        }

        public IDataResult<List<Role>> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            if (roles == null)
            {
                return new ErrorDataResult<List<Role>>(Messages.RolesNotFound);
            }
            return new SuccessDataResult<List<Role>>(roles);
        }

        public async Task<IDataResult<Role>> FindRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
            {
                return new ErrorDataResult<Role>(Messages.RoleNotFound);
            }
            return new SuccessDataResult<Role>(role);
        }

        public async Task<IResult> UpdateRoleAsync(Role role)
        {
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.RoleUpdated);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IResult> DeleteRoleAsync(Role role)
        {
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.RoleDeleted);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IResult> ChangeUserRoleAsync(User user, string userRole)
        {
            var getUserRoles = await GetUserRolesAsync(user);
            if (!getUserRoles.Success)
            {
                return new ErrorDataResult<Role>(getUserRoles.Message);
            }
            IResult result = BusinessRules.Run(await RemoveUserRoleAsync(user,getUserRoles.Data),await AddToRoleAsync(user,userRole));

            if (result != null)
            {
                return result;
            }
            return new SuccessResult(Messages.UserRoleChanged);

        }

        public async Task<IResult> AddToRoleAsync(User user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.RoleAddToUser);
            }
            return new ErrorResult(Messages.RoleNotAddToUser);
        }

        public async Task<IDataResult<List<string>>> GetUserRolesAsync(User user)
        {
            var result = await _userManager.GetRolesAsync(user);
            
            if (result== null)
            {
                return new ErrorDataResult<List<string>>(Messages.UserRolesNotFound);
            }
            return new SuccessDataResult<List<string>>(result.ToList());
        }

        public async Task<IResult> RemoveUserRoleAsync(User user, List<string> userRoles)
        {
            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (result.Succeeded)
            {
                return new SuccessResult(Messages.UserRolesRemove);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IDataResult<List<User>>> GetUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            if (result==null)
            {
                return new ErrorDataResult<List<User>>(Messages.UsersNotFound);
            }
            return new SuccessDataResult<List<User>>(result);
        }

        public async Task<IDataResult<string>> GetUserRoleAsync(User user)
        {
            var result = await _userManager.GetRolesAsync(user);
            if (result!=null)
            {
                var role = result.FirstOrDefault();
                return new SuccessDataResult<string>(role);
            }
            return new ErrorDataResult<string>(Messages.RoleNotFound);
        }

        public async Task<IDataResult<User>> DeleteUserAsync(int id)
        {
            var user = await FindUserByUserIdAsync(id);
            if (!user.Success)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            user.Data.IsDeleted = true;
            var result = await UpdateUserAsync(user.Data);
            if (result.Success)
            {
                return new SuccessDataResult<User>(Messages.UserDeactive);
            }
            return new ErrorDataResult<User>(user.Data,Messages.Error);

        }

        public async Task<IDataResult<User>> ReviveUserAsync(int id)
        {
            var user = await FindUserByUserIdAsync(id);
            if (!user.Success)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            user.Data.IsDeleted = false;
            var result = await UpdateUserAsync(user.Data);
            if (result.Success)
            {
                return new SuccessDataResult<User>(Messages.UserDeactive);
            }
            return new ErrorDataResult<User>(user.Data, Messages.Error);

        }
    }
}
