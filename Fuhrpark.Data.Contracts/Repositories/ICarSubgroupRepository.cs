using Anthill.Common.Data.Contracts;
using Fuhrpark.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Contracts.Repositories
{
    public interface ICarSubgroupRepository : IRepository
    {
        Task<IEnumerable<CarSubgroup>> GetAll();

        Task Add(CarSubgroup carSubgroup);

        Task Update(CarSubgroup carSubgroup);

        Task<CarSubgroup> GetByName(string name, int id = 0);

        Task<CarSubgroup> GetById(int id);

        Task<IEnumerable<CarSubgroup>> GetByIds(IEnumerable<int> ids);
    }
}
