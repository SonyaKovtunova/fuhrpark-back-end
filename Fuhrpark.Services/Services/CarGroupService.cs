using Anthill.Common.Data.Contracts;
using Anthill.Common.Services;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Fuhrpark.Services.Services
{
    public class CarGroupService : AbstractService, ICarGroupService
    {
        public CarGroupService(IUnityContainer container, IFuhrparkDataContextManager dataContextManager) 
            : base(container, dataContextManager)
        {
        }

        public async Task Add(CarGroupAddDto carGroupDto)
        {
            var carGroupRepository = DataContextManager.CreateRepository<ICarGroupRepository>();

            var carGroup = await carGroupRepository.GetByName(carGroupDto.Name);

            if (carGroup != null)
            {
                throw new AddingException();
            }

            var carSubgroupRepository = DataContextManager.CreateRepository<ICarSubgroupRepository>();
            var carSubgroups = await carSubgroupRepository.GetByIds(carGroupDto.CarSubgroupIds);

            var newCarGroup = new CarGroup
            {
                Name = carGroupDto.Name,
                CreateDate = DateTime.Now,
                CarSubgroupInCarGroups = carSubgroups.Select(carSubgroup => new CarSubgroupInCarGroup
                {
                    CarSubgroup = carSubgroup
                })
                .ToList()
            };

            await carGroupRepository.Add(newCarGroup);
        }

        public async Task<IEnumerable<CarGroupDto>> GetAll()
        {
            var carGroupRepository = DataContextManager.CreateRepository<ICarGroupRepository>();
            var carGroups = await carGroupRepository.GetAll();
            return MapperFactory.CreateMapper<ICarGroupMapper>().MapCollectionToModel(carGroups);
        }

        public async Task Update(CarGroupUpdateDto carGroupDto)
        {
            var carGroupRepository = DataContextManager.CreateRepository<ICarGroupRepository>();

            var carGroup = await carGroupRepository.GetById(carGroupDto.Id);

            if (carGroup == null)
            {
                throw new ObjectNotFoundException();
            }

            var carGroupWithSameName = await carGroupRepository.GetByName(carGroupDto.Name);

            if (carGroupWithSameName != null)
            {
                throw new UpdatingException();
            }

            var carSubgroupRepository = DataContextManager.CreateRepository<ICarSubgroupRepository>();
            var carSubgroups = await carSubgroupRepository.GetByIds(carGroupDto.CarSubgroupIds);

            carGroup.Name = carGroupDto.Name;
            carGroup.UpdateDate = DateTime.Now;

            AddCarSubgroups(carGroup, carSubgroups);
            DeleteCarSubgroups(carGroup, carSubgroups);

            await carGroupRepository.Update(carGroup);
        }

        private void DeleteCarSubgroups(CarGroup carGroup, IEnumerable<CarSubgroup> carSubgroups)
        {
            var carSubgroupIdsToDelete = new List<int>();

            foreach (var carSubgroupInCarGroup in carGroup.CarSubgroupInCarGroups)
            {
                if (carSubgroups.Any(x => x.Id == carSubgroupInCarGroup.CarSubgroupId))
                {
                    carSubgroupIdsToDelete.Add(carSubgroupInCarGroup.CarSubgroupId);
                }
            }

            carGroup.CarSubgroupInCarGroups.RemoveAll(x => carSubgroupIdsToDelete.Contains(x.CarSubgroupId));
        }

        private void AddCarSubgroups(CarGroup carGroup, IEnumerable<CarSubgroup> carSubgroups)
        {
            foreach (var carSubgroup in carSubgroups)
            {
                if (!carGroup.CarSubgroupInCarGroups.Any(x => x.CarSubgroupId == carSubgroup.Id))
                {
                    carGroup.CarSubgroupInCarGroups.Add(new CarSubgroupInCarGroup
                    {
                        CarGroupId = carGroup.Id,
                        CarSubgroupId = carSubgroup.Id
                    });
                }
            }
        }
    }
}
