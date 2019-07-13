using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Fuhrpark.Host.Infrastructure;
using Fuhrpark.Host.Mappers;
using Fuhrpark.Host.Models;
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
        private readonly IUnityContainer _container;
        private readonly ILog _log;

        private readonly IAccountService _accountService;

        public AccountController(IUnityContainer container, ILog log, IAccountService accountService)
        {
            _container = container;
            _log = log;

            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody]UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var identity = await GetIdentity(model.Email, model.Password);

            if (identity == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var now = DateTime.UtcNow;

            var newToken = GenerateToken(identity.Claims);
            var refreshToken = GenerateRefreshToken();

            try
            {
                await _accountService.SaveRefreshToken(model.Email, refreshToken);
            }
            catch (ArgumentException)
            {
                return Unauthorized("Invalid email or password.");
            }
           
            var response = new
            {
                access_token = newToken,
                refresh_token = refreshToken,
                expires_date = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
            };

            return Ok(response);
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
                    throw new ArgumentException("Invalid refresh token");
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

            var userDto = _container.Resolve<IUserRegisterMapper>().MapFromModel(model);

            try
            {
                var newUser = await _accountService.Register(userDto);

                if (newUser == null)
                {
                    throw new ArgumentException("Invalid email or password.");
                }

                var identity = await GetIdentity(model.Email, model.Password);

                if (identity == null)
                {
                    throw new ArgumentException("Invalid email or password.");
                }

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
                throw new ArgumentException("Invalid token");
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

            if (user != null)
            {
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

            return null;
        }
    }
}