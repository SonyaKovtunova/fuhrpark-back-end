using Anthill.Common.Data.Contracts;
using Anthill.Common.Services;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
using Fuhrpark.Services.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Fuhrpark.Services.Services
{
    public class CarService : AbstractService, ICarService
    {
        public CarService(IUnityContainer container, IFuhrparkDataContextManager dataContextManager) : base(container, dataContextManager)
        {
        }

        public async Task AddCar(CarAddDto carDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();
            var car = MapperFactory.CreateMapper<ICarAddMapper>().MapFromModel(carDto);

            car.CarBusiness.CreateDate = DateTime.Now;

            await carRepository.AddCar(car);
        }

        public async Task<CarDto> GetCarById(int id)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarById(id);

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            return MapperFactory.CreateMapper<ICarMapper>().MapToModel(car);
        }

        public async Task<CarRemoveInfoDto> RemoveCar(RemoveCarSettingsDto removeCarSettings)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarById(removeCarSettings.CarId);

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            var carWithSameManufacturer = await carRepository.GetCarByManufacturerId(car.ManufacturerId, removeCarSettings.CarId);
            var carWithSameTyp = await carRepository.GetCarByTypId(car.TypId, removeCarSettings.CarId);
            var carWithSameFuel = await carRepository.GetCarByFuelId(car.CarSpec.FuelId, removeCarSettings.CarId);
            var carWithSameEngineOil = await carRepository.GetCarByEngineOilId(car.CarSpec.EngineOilId, removeCarSettings.CarId);
            var carWithSameGearOil = await carRepository.GetCarByGearOilId(car.CarSpec.GearOilId, removeCarSettings.CarId);
            Car carWithSameUser = null;

            if (car.CarBusiness.UserId.HasValue)
            {
                carWithSameUser = await carRepository.GetCarByUserId(car.CarBusiness.UserId.Value, removeCarSettings.CarId);
            }

            if (!removeCarSettings.IsCheck)
            {
                await carRepository.RemoveCar(car);

                await RemoveRelationEntities(removeCarSettings, carWithSameManufacturer, carWithSameTyp, 
                    carWithSameFuel, carWithSameGearOil, carWithSameEngineOil, carWithSameUser, car);
            }

            return new CarRemoveInfoDto
            {
                WithSameManufacturer = carWithSameManufacturer != null ? true : false,
                WithSameTyp = carWithSameTyp != null ? true : false,
                WithSameFuel = carWithSameFuel != null ? true : false,
                WithSameEngineOil = carWithSameEngineOil != null ? true : false,
                WithSameGearOil = carWithSameGearOil != null ? true : false,
                WithSameUser = carWithSameUser != null ? true : false
            };
        }

        private async Task RemoveRelationEntities(RemoveCarSettingsDto removeCarSettings, Car carWithSameManufacturer, 
            Car carWithSameTyp, Car carWithSameFuel, Car carWithSameGearOil, Car carWithSameEngineOil, Car carWithSameUser, Car currentCar)
        {
            if (carWithSameManufacturer == null && removeCarSettings.ManufacturerIsDelete && currentCar.Manufacturer != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<Manufacturer>>();
                await repository.Remove(currentCar.Manufacturer);
            }

            if (carWithSameTyp == null && removeCarSettings.TypIsDelete && currentCar.Typ != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<Typ>>();
                await repository.Remove(currentCar.Typ);
            }

            if (carWithSameFuel == null && removeCarSettings.FuelIsDelete && currentCar.CarSpec.Fuel != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<Fuel>>();
                await repository.Remove(currentCar.CarSpec.Fuel);
            }

            if (carWithSameGearOil == null && removeCarSettings.GearOilIsDelete && currentCar.CarSpec.GearOil != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<GearOil>>();
                await repository.Remove(currentCar.CarSpec.GearOil);
            }

            if (carWithSameEngineOil == null && removeCarSettings.EngineOilIsDelete && currentCar.CarSpec.EngineOil != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<EngineOil>>();
                await repository.Remove(currentCar.CarSpec.EngineOil);
            }

            if (carWithSameUser == null && removeCarSettings.UserIsDelete && currentCar.CarBusiness.User != null)
            {
                var repository = DataContextManager.CreateRepository<ICommonRepository<User>>();
                await repository.Remove(currentCar.CarBusiness.User);
            }
        }

        public async Task UpdateCar(CarUpdateDto carDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarById(carDto.Id);

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            car.TypId = carDto.TypId;
            car.ManufacturerId = carDto.ManufacturerId;
            car.RegistrationNumber = carDto.RegistrationNumber;
            car.Model = carDto.Model;
            car.Decommissioned = carDto.Decommissioned;
            car.Color = carDto.Color;
            car.ChassisNumber = carDto.ChassisNumber;

            car.CarSpec.Catalyst = carDto.CarSpec.Catalyst;
            car.CarSpec.EngineCode = carDto.CarSpec.EngineCode;
            car.CarSpec.EngineDisplacement = carDto.CarSpec.EngineDisplacement;
            car.CarSpec.EngineOilId = carDto.CarSpec.EngineOilId;
            car.CarSpec.FuelId = carDto.CarSpec.FuelId;
            car.CarSpec.GearOilId = carDto.CarSpec.GearOilId;
            car.CarSpec.HybridDrive = carDto.CarSpec.HybridDrive;
            car.CarSpec.MaxSpeed = carDto.CarSpec.MaxSpeed;
            car.CarSpec.Performance = carDto.CarSpec.Performance;
            car.CarSpec.ProductionDate = carDto.CarSpec.ProductionDate;
            car.CarSpec.RegistrationDate = carDto.CarSpec.RegistrationDate;
            car.CarSpec.TotalWeight = carDto.CarSpec.TotalWeight;

            car.CarBusiness.Location = carDto.CarBusiness.Location;
            car.CarBusiness.UpdateDate = DateTime.Now;
            car.CarBusiness.UserId = carDto.CarBusiness.UserId;

            await carRepository.UpdateCar(car);
        }

        public async Task<IEnumerable<CarDto>> GetCars(IEnumerable<SearchAttributeDto> searchAttributes)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var carQuery = carRepository.GetCars();

            carQuery = carQuery.Filter<Car>(searchAttributes.Where(x => x.CarField >= Enums.CarField.RegistrationNumber && x.CarField <= Enums.CarField.Decommissioned).ToList());

            var carSpecQuery = carQuery.Select(s => s.CarSpec).AsQueryable()
                .Filter<CarSpec>(searchAttributes.Where(x => x.CarField >= Enums.CarField.FuelId && x.CarField <= Enums.CarField.HybridDrive).ToList());

            var carBusinessQuery = carQuery.Select(s => s.CarBusiness).AsQueryable()
                .Filter<CarBusiness>(searchAttributes.Where(x => x.CarField >= Enums.CarField.Location && x.CarField <= Enums.CarField.UpdateDate).ToList());

            var cars = await carQuery
                .Where(x => carSpecQuery.Select(s => s.Id).Contains(x.Id)
                            && carBusinessQuery.Select(s => s.Id).Contains(x.Id))
                .ToListAsync();

            return MapperFactory.CreateMapper<ICarMapper>().MapCollectionToModel(cars);
        }
    }
}
