using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Email;
using Email.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Projeto.Data;
using Projeto.Dominio;
using Projeto.Models;
using Projeto.Repositorio;
using Projeto.Services;

namespace Projeto.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ISendEmail _sendEmail;
        private readonly IUsersData _userData;

        public DefaultController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, 
                                ISendEmail sendEmail, IUsersData userData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _sendEmail = sendEmail;
            _userData = userData;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (UserModel userModel)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userModel.UserName);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = userModel.UserName,
                        Email = userModel.UserName,
                        Nome = userModel.FullName
                    };

                    var result = await _userManager.CreateAsync(user, userModel.Password);

                    if (result.Succeeded)
                    {
                        //var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                        //var confirmationEmail = Url.Action("ConfirmEmailAddress", "Default",
                        //    new { token = token, email = user.Email }, Request.Scheme);

                        //var emailResult = _sendEmail.Gmail(new EmailModel 
                        //{
                        //    AddresseeName = user.UserName,
                        //    AddresseeEmail = user.Email,
                        //    Subject = "Confirmação de email",
                        //    Message = confirmationEmail
                        //});

                        //if (!emailResult.Success)
                        //    return BadRequest(emailResult.Message);

                        return Ok(new { result = $"200" });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }

                return Unauthorized();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginModal)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLoginModal.UserName);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginModal.Password, false);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                        var refreshToken = Guid.NewGuid().ToString();

                        Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions()
                        {
                            HttpOnly = true,
                            Domain = "",
                            Path = "/",
                            SameSite = SameSiteMode.Strict
                        });

                        return Ok(new
                        {
                            token = _tokenService.GenerateToken(appUser).Result,
                            userInfo = new { user = appUser.Nome, email = appUser.Email }
                        });
                    }
                }

                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"ERROR {ex.Message}");
            }
        }

        [HttpGet("Update")]
        public async Task<IActionResult> UpdateToken()
        {
            return Unauthorized();
        }


        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                {
                    return Ok("Sucesso!");
                }
                else
                {
                    return BadRequest("Erro!");
                }
            }

            return Unauthorized();
        }

        [HttpGet("Authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado {_userManager.GetUserId(User)}";

        [HttpGet("Vagas")]
        [Authorize]
        public User Vagas()
        {
            return _userData.GetAllUsers();
        }
    }
}
