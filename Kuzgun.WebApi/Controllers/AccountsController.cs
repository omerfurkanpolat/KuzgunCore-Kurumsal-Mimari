using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Core.Entity.Concrete;
using Kuzgun.Core.Utilities.EmailService.Smtp;
using Kuzgun.Core.Utilities.Security;
using Kuzgun.Entities.ComplexTypes.UsersDTO;
using Kuzgun.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Kuzgun.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private IEmailService _emailService;
        private SignInManager<User> _signInManager;
        private IAuthService _authService;
        

        public AccountsController(UserManager<User> userManager, RoleManager<Role> roleManager,
            IEmailService emailService, SignInManager<User> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Eksik Bilgi Doldurdunuz");
            }

            
            //var uniqeEmailCheck = _userManager.FindByEmailAsync(model.Email);
            //var uniqueUserNameCheck = _userManager.FindByNameAsync(model.UserName);
            //if (uniqeEmailCheck != null || uniqueUserNameCheck != null)
            //{
            //    return BadRequest("Bu kullanıcı adı veya email adresi daha önce kullamış");
            //}

            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                IsDeleted = false

            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest("Kullanıcı Oluşturulamadı");
            }

            var role = "user";
            if (!_roleManager.RoleExistsAsync(role).Result)
            {
                var newRole = new Role();
                newRole.Name = role;
                await _roleManager.CreateAsync(newRole);
            }

            await _userManager.AddToRoleAsync(user, role);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = "http://localhost:4200/confirmEmail?";
            var callbackUrl = $"{url}" + $"UserId={user.Id}&" + $"Code={code}";

            _emailService.SendMail(user.UserName, user.Email, "Hesap onaylama maili",
                $"Hesabınızı onaylamak için lütfen linkle tıklayın: {callbackUrl} ");

            return Ok("Hesabınıza gelen emaili onaylayınız");
        }

        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(UserForConfirmEmailDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            var code = model.Code.Replace(" ", "+");
            if (user != null && code != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {
                    return Ok("Email Adresiniz Onayladı");
                }

                return BadRequest("Email adresiniz onaylanamadı");
            }

            return BadRequest("Bir hata oluştu");

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }

            if (user.IsDeleted == true)
            {
                return BadRequest("Bu hesaba erişim engellenmiştir");
            }

            if (user.EmailConfirmed == false)
            {
                return BadRequest("Email hesabınızı onaylamadan giriş yapamazsınız");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest("Giriş yapılamadı");
            }
            user.LastActive = DateTime.Now;
            await _userManager.UpdateAsync(user);

            var createToken= await _authService.CreateAccessToken(user);
            if (createToken.Success)
            {
                return Ok(createToken.Data);
            }

            return BadRequest("Bir hata oluştu");





        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserForForgotPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {

                    return BadRequest("Kullanıcı Bulunamadı");
                }

                if (!(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return BadRequest("Email Adresinizi Onaylayın");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var url = "http://localhost:4200/resetpassword?";
                var callbackUrl = $"{url}" + $"UserId={user.Id}&" + $"Code={code}";
                _emailService.SendMail(user.UserName, user.Email, "Parola Sıfırlama Maili",
                    $"Parolanızı sıfırlamak için lütfen linkle tıklayın {callbackUrl} ");

                return Ok();
            }

            return BadRequest(ModelState);

        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword(UserForResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {

                return BadRequest("Kullanıcı Bulunamadı");
            }

            var revisedCode = model.Code.Replace(" ", "+");
            var result = await _userManager.ResetPasswordAsync(user, revisedCode, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest("Bir Hata Oluştu");
        }

        [HttpPut]
        [Route("changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(UserForChangePasswordDTO model, int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                return Ok();
            }
            return BadRequest("Mecvut şifrenizi hatalı girdiniz");
        }

        [HttpGet]
        [Route("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            //var result = _mapper.Map<UserForChangeEmailDTO>(user);
            return Ok(user);
        }

        [HttpPut]
        [Route("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(UserForChangeEmailDTO model, int id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Email Adresi Değiştirilemedi");
            }
            user.Email = model.Email;
            await _userManager.UpdateAsync(user);
            return Ok();

        }

        [HttpPut]
        [Route("changeProfilePicture/{id}")]
        public async Task<IActionResult> ChangeProfilPicture(UserForChangeProfilePictureDTO model, int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Profil Resmi Değiştirilemedi");
            }
            user.ImageUrl = model.ImageUrl;
            await _userManager.UpdateAsync(user);
            return Ok();
        }


    }
}