using Anthill.Common.Data.Contracts;
using Anthill.Common.Services;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Fuhrpark.Services.Services
{
    public class AccountService : AbstractService, IAccountService
    {
        public AccountService(IUnityContainer container, IFuhrparkDataContextManager dataContextManager) : base(container, dataContextManager)
        {
        }

        public async Task<string> GetRefreshToken(string email)
        {
            var accountRepository = DataContextManager.CreateRepository<IAccountRepository>();
            var user = await accountRepository.GetByEmail(email);

            if (user == null)
            {
                throw new ArgumentException();
            }

            return user.RefreshToken;
        }

        public async Task<AppUserDto> GetUserByEmail(string email)
        {
            var accountRepository = DataContextManager.CreateRepository<IAccountRepository>();

            var user = await accountRepository.GetByEmail(email);

            return MapperFactory.CreateMapper<IAppUserMapper>().MapToModel(user);
        }

        public async Task<AppUserDto> Login(string email, string password)
        {
            var accountRepository = DataContextManager.CreateRepository<IAccountRepository>();
            var user = await accountRepository.GetByEmail(email);

            if (user == null)
            {
                return null;
            }

            if (!user.Password.Equals(GetMD5Hash(password), StringComparison.InvariantCulture))
            {
                return null;
            }

            return MapperFactory.CreateMapper<IAppUserMapper>().MapToModel(user);
        }

        public async Task<AppUserDto> Register(AppUserDto userDto)
        {
            var accountRepository = DataContextManager.CreateRepository<IAccountRepository>();
            var user = await accountRepository.GetByEmail(userDto.Email);

            if (user != null)
            {
                throw new ArgumentException("User with same email exists.");
            }

            user = new AppUser
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Mobile = userDto.Mobile,
                Password = GetMD5Hash(userDto.Password),
                CreateDate = DateTime.Now,
                IsActive = true
            };

            await accountRepository.Add(user);
            await DataContextManager.SaveAsync();

            return MapperFactory.CreateMapper<IAppUserMapper>().MapToModel(user);
        }

        public async Task SaveRefreshToken(string email, string refreshToken)
        {
            var accountRepository = DataContextManager.CreateRepository<IAccountRepository>();
            var user = await accountRepository.GetByEmail(email);

            if (user == null)
            {
                throw new ArgumentException("User with this email doesn't exist.");
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpires = DateTime.Now.AddHours(2);
            await DataContextManager.SaveAsync();
        }

        private string GetMD5Hash(string password)
        {
            var md5 = System.Security.Cryptography.MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sb = new StringBuilder();

            for (Int32 i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
