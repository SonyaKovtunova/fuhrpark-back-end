using Anthill.Common.Data.Contracts;
using Fuhrpark.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Contracts.Repositories
{
    public interface IAccountRepository : IRepository
    {
        Task<AppUser> GetById(Int32 id);

        Task<AppUser> GetByEmail(string email);

        Task Add(AppUser user);

        Task SaveRefreshToken(AppUser user);
    }
}
