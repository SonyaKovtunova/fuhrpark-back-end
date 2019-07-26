using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Fuhrpark.Enums;
using Fuhrpark.Host.Helpers;
using Fuhrpark.Host.Infrastructure;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Unity;

namespace Fuhrpark.Host.Controllers
{
    [Route("api/account")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class AccountController : ControllerBase
    {
        private readonly ILog _log;

        private readonly IAccountService _accountService;
        private readonly IEmailSender _emailSender;

        private readonly AppSettings _appSettings;

        public AccountController(ILog log, IAccountService accountService, IEmailSender emailSender, AppSettings appSettings)
        {
            _log = log;

            _accountService = accountService;
            _emailSender = emailSender;

            _appSettings = appSettings;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody]UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var identity = await GetIdentity(model.Email, model.Password);

                var now = DateTime.UtcNow;

                var newToken = GenerateToken(identity.Claims);
                var refreshToken = GenerateRefreshToken();

                await _accountService.SaveRefreshToken(model.Email, refreshToken);

                var response = new
                {
                    access_token = newToken,
                    refresh_token = refreshToken,
                    expires_date = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
                };

                return Ok(response);
            }
            catch (ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
            }
            catch(UnauthorizedAccessException uaex)
            {
                _log.Error(uaex);
            }
            catch(ArgumentException aex)
            {
                _log.Error(aex);
            }

            return Unauthorized(ErrorMessage.INVALIDEMAILORPASSWORD.ToString());
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken([FromBody]UserRefreshTokenModel model)
        {
            try
            {
                var principal = GetPrincipalFromExpiredToken(model.Token);
                var email = principal.Identity.Name;
                var savedRefreshToken = await _accountService.GetRefreshToken(email);

                if (savedRefreshToken != model.RefreshToken)
                {
                    throw new ArgumentException(ErrorMessage.INVALIDREFRESHTOKEN.ToString());
                }
                    
                var newJwtToken = GenerateToken(principal.Claims);
                var newRefreshToken = GenerateRefreshToken();

                await _accountService.SaveRefreshToken(email, newRefreshToken);

                var now = DateTime.UtcNow;

                var response = new
                {
                    access_token = newJwtToken,
                    refresh_token = newRefreshToken,
                    expires_date = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
                };

                return Ok(response);
            }
            catch (ArgumentException aex)
            {
                return Unauthorized(aex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody]UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = new AppUserDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Mobile = model.Mobile,
            };

            try
            {
                var newUser = await _accountService.Register(userDto);

                var identity = await GetIdentity(model.Email, model.Password);

                var now = DateTime.UtcNow;

                var newToken = GenerateToken(identity.Claims);
                var refreshToken = GenerateRefreshToken();
                await _accountService.SaveRefreshToken(model.Email, refreshToken);

                var response = new
                {
                    access_token = newToken,
                    refresh_token = refreshToken,
                    expires_date = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
                };

                return Ok(response);

            }
            catch (ArgumentException aex)
            {
                _log.Error(aex);
                return Unauthorized(aex.Message);
            }
            catch(ObjectNotFoundException onfex)
            {
                _log.Error(onfex);
            }
            catch(UnauthorizedAccessException uaex)
            {
                _log.Error(uaex);
            }

            return Unauthorized(ErrorMessage.INVALIDEMAILORPASSWORD.ToString());
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassord([FromBody]UserForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _accountService.GetUserByEmail(model.Email);

            if (user == null)
            {
                return Unauthorized(ErrorMessage.INVALIDEMAILORPASSWORD.ToString());
            }

            var token = GenerateForgotPassowrdCodeToken();

            try
            {
                await _accountService.SaveForgotPasswordCodeToken(user.Email, token);

                var resetPasswordLink = _appSettings.FrontEndUrl + String.Format(Constants.NotificationMessages.ResetPasswordUrl, token);
                var userName = String.Format(Constants.NotificationMessages.UserName, user.FirstName, user.LastName);
                var message = String.Format(Constants.NotificationMessages.ForgotPasswordMessage, userName, _appSettings.FrontEndUrl, resetPasswordLink);

                await _emailSender.SendEmailAsync(new MailAddress(user.Email, userName), Constants.NotificationMessages.ForgotPasswordSubject, message);

                var response = new
                {
                    forgot_password_token = token
                };

                return Ok(response);
            }
            catch(ArgumentException aex)
            {
                _log.Error(aex);
                return Unauthorized(aex.Message);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody]UserResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var forgotPasswordCodeToken = await _accountService.GetForgotPasswordCodeToken(model.Email);

                if (!model.Code.Equals(forgotPasswordCodeToken))
                {
                    return Unauthorized(ErrorMessage.INVALIDFORGOTPASSWORDTOKEN.ToString());
                }

                await _accountService.ResetPassword(model.Email, model.Password);

                return Ok();

            }
            catch (ArgumentException aex)
            {
                _log.Error(aex);
                return Unauthorized(aex.Message);
            }
            
        }

        [HttpGet]
        [Route("user-info")]
        [Authorize(Policy = "Bearer")]
        public async Task<ActionResult> GetUserUnfo()
        {
            var email = User.Identity.Name;

            var userDto = await _accountService.GetUserByEmail(email);

            var response = new
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = User.Identity.Name,
                HasRegistered = User.Identity.IsAuthenticated,
                Mobile = userDto.Mobile
            };

            return Ok(response);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ArgumentException(ErrorMessage.INVALIDTOKEN.ToString());
            }
                
            return principal;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateForgotPassowrdCodeToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var user = await _accountService.Login(email, password);

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                    new Claim("UserId", user.Id.ToString())
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}