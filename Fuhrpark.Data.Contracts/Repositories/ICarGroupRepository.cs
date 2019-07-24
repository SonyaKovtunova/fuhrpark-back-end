using Anthill.Common.Data.Contracts;
using Fuhrpark.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Contracts.Repositories
{
    public interface ICarGroupRepository : IRepository
    {
        Task<IEnumerable<CarGroup>> GetAll();

        Task Add(CarGroup carGroup);

        Task Update(CarGroup carGroup);

        Task<CarGroup> GetByName(string name, int id = 0);

        Task<CarGroup> GetById(int id);
    }
}
