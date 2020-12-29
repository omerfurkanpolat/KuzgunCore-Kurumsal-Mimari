using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Kuzgun.Bussines.Abstract;
using Kuzgun.Bussines.ValidationRules.FluentValidation.User;
using Kuzgun.Core.Aspects.Autofac.Validation;
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
       
        private IAuthService _authService;
        private IMapper _mapper;


        public AccountsController( IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            var result = await _authService.CreateUserAsync(model);
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
            var result = await _authService.ConfirmEmailAsync(model);
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

            var signInCheck = await _authService.LoginAsync(model);
            if (!signInCheck.Success)
            {
                return BadRequest(signInCheck.Message);

            }

            var createToken = await _authService.CreateAccessToken(signInCheck.Data);
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
            
            var result = await _authService.ForgotPasswordAsync(model);
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
           
            var result =await _authService.ResetPasswordAsync(model);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Success);
        }

        [HttpPut]
        [Route("changePassword/{id}")]
        public async Task<IActionResult> ChangePassword(UserForChangePasswordDTO model, int id)
        {
            var result = await _authService.ChangePasswordAsync(model, id);
            if (result.Success)
            {
                return Ok(result.Message);

            }

            return BadRequest(result.Message);
        }

        [HttpGet]
        [Route("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(int id)
        {
            var result = await _authService.FindUserByUserIdAsync(id);
            if (result.Success)
            {
                var user = _mapper.Map<UserForChangeEmailDTO>(result.Data);
                return Ok(user);
            }

            return BadRequest(result.Message);

        }

        [HttpPut]
        [Route("changeEmail/{id}")]
        public async Task<IActionResult> ChangeEmail(UserForChangeEmailDTO model, int id)
        {
            var result = await _authService.ChangeEmailAddressAsync(model, id);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);

        }

        [HttpPut]
        [Route("changeProfilePicture/{id}")]
        public async Task<IActionResult> ChangeProfilePicture(UserForChangeProfilePictureDTO model, int id)
        {
            var result = await _authService.ChangeProfilePictureAsync(model, id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


    }
}