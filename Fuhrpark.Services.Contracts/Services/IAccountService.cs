using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface IAccountService
    {
        Task SaveRefreshToken(String email, String refreshToken);

        Task<String> GetRefreshToken(String email);

        Task<AppUserDto> Register(AppUserDto user);

        Task<AppUserDto> Login(string email, string password);

        Task<AppUserDto> GetUserByEmail(string email);
    }
}
