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
            var roles = await ChangeRoleType(roleList.ToList());

            var accessToken = _tokenHelper.CreateToken(user, roles);
            if (accessToken == null)
            {
                return new ErrorDataResult<AccessToken>(Messages.AccessTokenNotCreated);
            }
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public async Task<List<Role>> ChangeRoleType(List<string> roleList)
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
        public async Task<IResult> CreateUser(UserForRegisterDTO userForRegisterDto)
        {
            var roleName = "user";

            IResult result = BusinessRules.Run(await CheckIfUserNameExists(userForRegisterDto.UserName), await CheckIfEmailExists(userForRegisterDto.Email),
                await CreateRole(roleName));

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

        public async Task<IResult> CheckIfUserNameExists(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result != null)
            {
                return new ErrorResult(Messages.UserNameAlreadyExists);
            }
            return new SuccessResult();

        }

        public async Task<IResult> CheckIfEmailExists(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result != null)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);

            }
            return new SuccessResult();


        }

        public async Task<IResult> CreateRole(string roleName)
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
        public async Task<IResult> ConfirmEmail(UserForConfirmEmailDTO userForConfirmEmailDto)
        {
            IDataResult<User> result = BusinessDataRules.Run(await FindByUserId(userForConfirmEmailDto.UserId));
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

        public async Task<IDataResult<User>> FindByUserId(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(user);
        }

        [ValidationAspect(typeof(UserForLoginDtoValidator))]
        public async Task<IResult> Login(UserForLoginDTO userForLoginDto)
        {
            var user = await FindByUserName(userForLoginDto.UserName);
            if (!user.Success)
            {
                return user;
            }

            IResult result = BusinessRules.Run(IsEmailConfirmed(user.Data),IsEmailConfirmed(user.Data));
            if (result != null)
            {
                return result;
            }

            var singInAccount = await _signInManager.CheckPasswordSignInAsync(user.Data, userForLoginDto.Password, false);
            if (!singInAccount.Succeeded)
            {
                return new ErrorDataResult<User>(Messages.UserNameOrPasswordWrong);
            }
            user.Data.LastActive = DateTime.Now;
            await UpdateUser(user.Data);

            return new SuccessDataResult<User>(user.Data);

        }

        public async Task<IDataResult<User>> FindByUserName(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);
        }

        public IResult UserIsDeleted(User user)
        {
            if (user.IsDeleted == true)
            {
                return new ErrorResult(Messages.UserIsDeleted);
            }
            return new SuccessResult();
        }

        public IResult IsEmailConfirmed(User user)
        {
            if (user.EmailConfirmed == true)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.EmailNotConfirmed);
        }

        [ValidationAspect(typeof(UserForForgotPasswordDtoValidator))]
        public async Task<IResult> ForgotPassword(UserForForgotPasswordDTO userForForgotPasswordDto)
        {
            var user = await FindByEmail(userForForgotPasswordDto.Email);
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

        public async Task<IDataResult<User>> FindByEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if (result == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            return new SuccessDataResult<User>(result);
        }

        [ValidationAspect(typeof(UserForResetPasswordDTO))]
        public async Task<IResult> ResetPassword(UserForResetPasswordDTO userForResetPasswordDto)
        {
            IDataResult<User> user = await FindByUserId(userForResetPasswordDto.UserId);
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

        public async Task<IResult> ChangePassword(UserForChangePasswordDTO userForChangePasswordDto, int id)
        {
            IDataResult<User> user = await FindByUserId(id);
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

        public async Task<IDataResult<User>> UpdateUser(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new SuccessDataResult<User>();
            }
            return new ErrorDataResult<User>(Messages.UserNotUpdate);
        }

        [ValidationAspect(typeof(UserForChangeEmailDtoValidator))]
        public async Task<IResult> ChangeEmailAddress(UserForChangeEmailDTO userForChangeEmailDto, int id)
        {
            var user = await FindByUserId(id);
            if (!user.Success)
            {
                return user;
            }

            user.Data.Email = userForChangeEmailDto.Email;
            var result = await UpdateUser(user.Data);
            if (result.Success)
            {
                return new SuccessResult(Messages.EmailChanged);
            }
            return new ErrorResult(Messages.Error);
        }

        public async Task<IResult> ChangeProfilePicture(UserForChangeProfilePictureDTO changeProfilePictureDto, int id)
        {
            var user = await FindByUserId(id);
            if (!user.Success)
            {
                return user;
            }
            user.Data.ImageUrl = changeProfilePictureDto.ImageUrl;
            var result = await UpdateUser(user.Data);
            if (result.Success)
            {
                return new SuccessResult(Messages.ProfilePictureChanged);
            }
            return new ErrorResult(Messages.Error);
        }
    }
}
