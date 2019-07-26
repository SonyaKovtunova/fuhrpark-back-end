using Anthill.Common.Data;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Repositories
{
    public class AccountRepository : AbstractRepository, IAccountRepository
    {
        public AccountRepository(FuhrparkDataContext context) : base(context)
        {
        }

        public async Task Add(AppUser user)
        {
            await Context.Set<AppUser>().AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task<AppUser> GetByEmail(string email)
        {
            return await Context.Set<AppUser>().FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task<AppUser> GetById(int id)
        {
            return await Context.Set<AppUser>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(AppUser user)
        {
            Context.Entry<AppUser>(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
