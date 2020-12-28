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


            var result = await _authService.CreateUser(model);
            if (result.Success)
            {
                return Ok(result.Message);

            }

            return BadRequest(result.Message);


        }

        [HttpPost]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(UserForConfirmEmailDTO model)
        {
            var result = await _authService.ConfirmEmail(model);
            if (result.Success)
            {
                return Ok(result.Message);

            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginDTO model)
        {
            var user = await _authService.GetUserByUserName(model.UserName);
            if (user == null)
            {
                return BadRequest(user.Message);
            }

            var checkUserDeleted = _authService.UserIsDeleted(user.Data);
            if (!checkUserDeleted.Success)
            {
                return BadRequest(checkUserDeleted.Message);
            }

            var checkEmailConfirmed = _authService.IsEmailConfirmed(user.Data);
            if (!checkEmailConfirmed.Success)
            {
                return BadRequest(checkEmailConfirmed.Message);
            }


            var signInCheck = await _authService.Login(user.Data, model);
            if (!signInCheck.Success)
            {
                return BadRequest(signInCheck.Message);

            }

            var createToken = await _authService.CreateAccessToken(user.Data);
            if (createToken.Success)
            {
                return Ok(createToken.Data);
            }

            return BadRequest(createToken.Message);


        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(UserForForgotPasswordDTO model)
        {
            
            var result = await _authService.ForgotPassword(model);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

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
        public async Task<IActionResult> ChangeProfilePicture(UserForChangeProfilePictureDTO model, int id)
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