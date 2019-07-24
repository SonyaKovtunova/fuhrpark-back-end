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
    public class CommonRepository<TEntity> : AbstractRepository, ICommonRepository<TEntity> where TEntity : CommonEntity
    {
        public CommonRepository(FuhrparkDataContext context) : base(context)
        {
        }

        public async Task Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TEntity> GetByName(string name, int id = 0)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Name.Equals(name) && x.Id != id);
        }

        public async Task Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
