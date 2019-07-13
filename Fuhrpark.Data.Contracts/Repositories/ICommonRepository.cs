using Anthill.Common.Data.Contracts;
using Fuhrpark.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Contracts.Repositories
{
    public interface ICommonRepository<TEntity>: IRepository where TEntity: CommonEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task Add(TEntity entity);

        void Remove(TEntity entity);

        void Update(TEntity entity);

        Task<TEntity> GetByName(string name);
    }
}
