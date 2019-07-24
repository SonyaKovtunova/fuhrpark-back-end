using Anthill.Common.Data.Contracts;
using Anthill.Common.Services;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Data.Repositories;
using Fuhrpark.Enums;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Dtos.Common;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Fuhrpark.Services.Services
{
    public class CommonService<Dto, TEntity> : AbstractService, ICommonService<Dto> where Dto : CommonDto where TEntity : CommonEntity
    {
        public CommonService(IUnityContainer container, IFuhrparkDataContextManager dataContextManager) : base(container, dataContextManager)
        {
        }

        public async Task Add(CommonAddDto dto)
        {
            var repository = DataContextManager.CreateRepository<ICommonRepository<TEntity>>();

            var entity = await repository.GetByName(dto.Name);

            if (entity != null)
            {
                throw new AddingException();
            }

            entity = MapperFactory.CreateMapper<ICommonAddMap<TEntity, CommonAddDto>>().MapFromModel(dto);
            entity.CreateDate = DateTime.Now;

            await repository.Add(entity);
        }

        public async Task<IEnumerable<Dto>> GetAll()
        {
            var repository = DataContextManager.CreateRepository<ICommonRepository<TEntity>>();

            var entities = await repository.GetAll();

            return MapperFactory.CreateMapper<ICommonMapper<TEntity, Dto>>().MapCollectionToModel(entities);
        }

        public async Task Remove(int id, RemovalType removalType)
        {
            var repository = DataContextManager.CreateRepository<ICommonRepository<TEntity>>();

            var entity = await repository.GetById(id);

            if (entity == null)
            {
                throw new ObjectNotFoundException();
            }

            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarByEntityType(entity.Id, removalType);

            if (car != null)
            {
                throw new RemovingException();
            }

            await repository.Remove(entity.Id);
        }

        public async Task Update(CommonUpdateDto dto)
        {
            var repository = DataContextManager.CreateRepository<ICommonRepository<TEntity>>();

            var entity = await repository.GetById(dto.Id);

            if (entity == null)
            {
                throw new ObjectNotFoundException();
            }

            var existingEntity = await repository.GetByName(dto.Name, dto.Id);

            if (existingEntity != null)
            {
                throw new UpdatingException();
            }

            entity.Name = dto.Name;
            entity.UpdateDate = DateTime.Now;

            await repository.Update(entity);
        }
    }
}
