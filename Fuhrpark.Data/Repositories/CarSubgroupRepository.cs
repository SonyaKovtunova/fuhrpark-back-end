using Anthill.Common.Data;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Repositories
{
    public class CarSubgroupRepository : AbstractRepository, ICarSubgroupRepository
    {
        public CarSubgroupRepository(AbstractDataContext context) : base(context)
        {
        }

        public async Task Add(CarSubgroup carSubgroup)
        {
            await Context.Set<CarSubgroup>()
                .AddAsync(carSubgroup);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarSubgroup>> GetAll()
        {
            return await Context.Set<CarSubgroup>()
                .OrderByDescending(ord => ord.CreateDate)
                .ThenByDescending(ord => ord.UpdateDate)
                .ToListAsync();
        }

        public async Task<CarSubgroup> GetById(int id)
        {
            return await Context.Set<CarSubgroup>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CarSubgroup>> GetByIds(IEnumerable<int> ids)
        {
            return await Context.Set<CarSubgroup>()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<CarSubgroup> GetByName(string name, int id = 0)
        {
            return await Context.Set<CarSubgroup>()
                .FirstOrDefaultAsync(x => x.Name.Equals(name) && x.Id != id);
        }

        public async Task<IEnumerable<CarSubgroup>> GetSubgroupsByCarId(int carId)
        {
            return await Context.Set<CarSubgroup>()
                .Where(x => x.CarInCarSubgroups.Select(s => s.CarId).Contains(carId))
                .ToListAsync();
        }

        public async Task Update(CarSubgroup carSubgroup)
        {
            Context.Entry<CarSubgroup>(carSubgroup).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
