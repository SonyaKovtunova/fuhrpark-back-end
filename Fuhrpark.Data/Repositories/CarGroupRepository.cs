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
    public class CarGroupRepository : AbstractRepository, ICarGroupRepository
    {
        public CarGroupRepository(AbstractDataContext context) : base(context)
        {
        }

        public async Task Add(CarGroup carGroup)
        {
            await Context.Set<CarGroup>()
                .AddAsync(carGroup);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarGroup>> GetAll()
        {
            return await Context.Set<CarGroup>().ToListAsync();
        }

        public async Task<CarGroup> GetById(int id)
        {
            return await Context.Set<CarGroup>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CarGroup> GetByName(string name)
        {
            return await Context.Set<CarGroup>().FirstOrDefaultAsync(x => x.Name.Equals(name));
        }

        public async Task Update(CarGroup carGroup)
        {
            Context.Entry<CarGroup>(carGroup).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
