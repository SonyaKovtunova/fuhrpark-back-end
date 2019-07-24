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
    public class CarSubgroupService : AbstractService, ICarSubgroupService
    {
        public CarSubgroupService(IUnityContainer container, IFuhrparkDataContextManager dataContextManager) 
            : base(container, dataContextManager)
        {
        }

        public async Task Add(CarSubgroupAddDto carSubgroupDto)
        {
            var carSubgroupRepository = DataContextManager.CreateRepository<ICarSubgroupRepository>();

            var carSubgroup = await carSubgroupRepository.GetByName(carSubgroupDto.Name);

            if (carSubgroup != null)
            {
                throw new AddingException();
            }

            var carRepository = DataContextManager.CreateRepository<ICarRepository>();
            var cars = await carRepository.GetCarsByIds(carSubgroupDto.CarIds);

            var newCarSubgroup = new CarSubgroup
            {
                Name = carSubgroupDto.Name,
                CreateDate = DateTime.Now,
                CarInCarSubgroups = cars.Select(car => new CarInCarSubgroup
                {
                    Car = car
                })
                            .ToList()
            };

            await carSubgroupRepository.Add(newCarSubgroup);
        }

        public async Task<IEnumerable<CarSubgroupDto>> GetAll()
        {
            var carSubgroupRepository = DataContextManager.CreateRepository<ICarSubgroupRepository>();

            var carSubgroups = await carSubgroupRepository.GetAll();

            return MapperFactory.CreateMapper<ICarSubgroupMapper>().MapCollectionToModel(carSubgroups);
        }

        public async Task Update(CarSubgroupUpdateDto carSubgroupDto)
        {
            var carSubgroupRepository = DataContextManager.CreateRepository<ICarSubgroupRepository>();

            var carSubgroup = await carSubgroupRepository.GetById(carSubgroupDto.Id);

            if (carSubgroup == null)
            {
                throw new ObjectNotFoundException();
            }

            var carSubgroupWithSameName = await carSubgroupRepository.GetByName(carSubgroupDto.Name, carSubgroupDto.Id);

            if (carSubgroupWithSameName != null)
            {
                throw new UpdatingException();
            }

            var carRepository = DataContextManager.CreateRepository<ICarRepository>();
            var cars = await carRepository.GetCarsByIds(carSubgroupDto.CarIds);

            carSubgroup.Name = carSubgroupDto.Name;
            carSubgroup.UpdateDate = DateTime.Now;

            AddCars(carSubgroup, cars);
            DeleteCars(carSubgroup, cars);

            await carSubgroupRepository.Update(carSubgroup);
        }

        private void DeleteCars(CarSubgroup carSubgroup, IEnumerable<Car> cars)
        {
            var carIdsToDelete = new List<int>();

            foreach (var carInCarSubgroup in carSubgroup.CarInCarSubgroups)
            {
                if (!cars.Any(x => x.Id == carInCarSubgroup.CarId))
                {
                    carIdsToDelete.Add(carInCarSubgroup.CarId);
                }
            }

            carSubgroup.CarInCarSubgroups.RemoveAll(x => carIdsToDelete.Contains(x.CarId));
        }

        private void AddCars(CarSubgroup carSubgroup, IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            {
                if (!carSubgroup.CarInCarSubgroups.Any(x => x.CarId == car.Id))
                {
                    carSubgroup.CarInCarSubgroups.Add(new CarInCarSubgroup
                    {
                        CarId = car.Id,
                        CarSubgroupId = carSubgroup.Id
                    });
                }
            }
        }
    }
}
