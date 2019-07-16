using Anthill.Common.Data.Contracts;
using Anthill.Common.Services;
using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Exceptions;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
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

        public async Task<IEnumerable<CarDto>> GetCars(SearchFilterDto searchDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var cars = carRepository.GetCars();

            cars = SearchCarsByGeneralSettings(searchDto, cars);

            if (searchDto.CarSpec != null)
            {
                cars = SearchCarsBySpec(searchDto.CarSpec, cars);
            }

            if (searchDto.CarBusiness != null)
            {
                cars = SearchCarsByBusiness(searchDto.CarBusiness, cars);
            }
            
            var foundCars = await cars.ToListAsync();

            return MapperFactory.CreateMapper<ICarMapper>().MapCollectionToModel(foundCars);
        }

        private IQueryable<Car> SearchCarsByGeneralSettings(SearchFilterDto searchDto, IQueryable<Car> cars)
        {
            if (!String.IsNullOrEmpty(searchDto.ChassisNumber))
            {
                cars = cars.Where(x => x.ChassisNumber.Contains(searchDto.ChassisNumber));
            }

            if (!String.IsNullOrEmpty(searchDto.Color))
            {
                cars = cars.Where(x => x.Color.Contains(searchDto.Color));
            }

            if (searchDto.Decommissioned.HasValue)
            {
                cars = cars.Where(x => x.Decommissioned == searchDto.Decommissioned.Value);
            }

            if (searchDto.ManufacturerId.HasValue)
            {
                cars = cars.Where(x => x.ManufacturerId == searchDto.ManufacturerId.Value);
            }

            if (!String.IsNullOrEmpty(searchDto.Model))
            {
                cars = cars.Where(x => x.Model.Contains(searchDto.Model));
            }

            if (!String.IsNullOrEmpty(searchDto.RegistrationNumber))
            {
                cars = cars.Where(x => x.RegistrationNumber.Contains(searchDto.RegistrationNumber));
            }

            if (searchDto.TypId.HasValue)
            {
                cars = cars.Where(x => x.TypId == searchDto.TypId);
            }

            return cars;
        }

        private IQueryable<Car> SearchCarsBySpec(SearchFilterDto.CarSpecSearchDto carSpec, IQueryable<Car> cars)
        {
            if (carSpec.Catalyst.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.Catalyst == carSpec.Catalyst.Value);
            }

            if (!String.IsNullOrEmpty(carSpec.EngineCode))
            {
                cars = cars.Where(x => x.CarSpec.EngineCode.Contains(carSpec.EngineCode));
            }

            if (carSpec.EngineOilId.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.EngineOilId == carSpec.EngineOilId.Value);
            }

            if (carSpec.FuelId.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.FuelId == carSpec.FuelId.Value);
            }

            if (carSpec.GearOilId.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.GearOilId == carSpec.GearOilId.Value);
            }

            if (carSpec.HybridDrive.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.HybridDrive == carSpec.HybridDrive.Value);
            }

            if (carSpec.MaxEngineDisplacement.HasValue && carSpec.MinEngineDisplacement.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue
                        && carSpec.MinEngineDisplacement.Value <= x.CarSpec.EngineDisplacement.Value
                        && x.CarSpec.EngineDisplacement.Value <= carSpec.MaxEngineDisplacement.Value);
            }
            else if (carSpec.MaxEngineDisplacement.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue
                        && x.CarSpec.EngineDisplacement.Value <= carSpec.MaxEngineDisplacement.Value);
            }
            else if (carSpec.MinEngineDisplacement.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue
                        && carSpec.MinEngineDisplacement.Value <= x.CarSpec.EngineDisplacement.Value);
            }

            if (carSpec.MaxPerformance.HasValue && carSpec.MinPerformance.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.Performance.HasValue
                        && carSpec.MinPerformance.Value <= x.CarSpec.Performance.Value
                        && x.CarSpec.Performance.Value <= carSpec.MaxPerformance.Value);
            }
            else if (carSpec.MaxPerformance.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.Performance.HasValue
                        && x.CarSpec.Performance.Value <= carSpec.MaxPerformance.Value);
            }
            else if (carSpec.MinPerformance.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.Performance.HasValue
                        && carSpec.MinPerformance.Value <= x.CarSpec.Performance.Value);
            }

            if (carSpec.MaxSpeed.HasValue && carSpec.MinSpeed.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                        && carSpec.MinSpeed.Value <= x.CarSpec.MaxSpeed.Value
                        && x.CarSpec.MaxSpeed.Value <= carSpec.MaxSpeed.Value);
            }
            else if (carSpec.MaxSpeed.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                        && x.CarSpec.MaxSpeed.Value <= carSpec.MaxSpeed.Value);
            }
            else if (carSpec.MinSpeed.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                        && carSpec.MinSpeed.Value <= x.CarSpec.MaxSpeed.Value);
            }

            if (carSpec.MaxTotalWeight.HasValue && carSpec.MinTotalWeight.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                        && carSpec.MinTotalWeight.Value <= x.CarSpec.TotalWeight.Value
                        && x.CarSpec.TotalWeight.Value <= carSpec.MaxTotalWeight.Value);
            }
            else if (carSpec.MaxTotalWeight.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                        && x.CarSpec.TotalWeight.Value <= carSpec.MaxTotalWeight.Value);
            }
            else if (carSpec.MinTotalWeight.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                        && carSpec.MinTotalWeight.Value <= x.CarSpec.TotalWeight.Value);
            }

            if (carSpec.ProductionStartDate.HasValue && carSpec.ProductionEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                && carSpec.ProductionStartDate.Value <= x.CarSpec.ProductionDate.Value
                                && x.CarSpec.ProductionDate.Value <= carSpec.ProductionEndDate.Value);
            }
            else if (carSpec.ProductionStartDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                && carSpec.ProductionStartDate.Value <= x.CarSpec.ProductionDate.Value);
            }
            else if (carSpec.ProductionEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                && x.CarSpec.ProductionDate.Value <= carSpec.ProductionEndDate.Value);
            }

            if (carSpec.RegistrationStartDate.HasValue && carSpec.RegistrationEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                && carSpec.RegistrationStartDate.Value <= x.CarSpec.RegistrationDate.Value
                                && x.CarSpec.RegistrationDate.Value <= carSpec.RegistrationEndDate.Value);
            }
            else if (carSpec.RegistrationStartDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                && carSpec.RegistrationStartDate.Value <= x.CarSpec.RegistrationDate.Value);
            }
            else if (carSpec.RegistrationEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                && x.CarSpec.RegistrationDate.Value <= carSpec.RegistrationEndDate.Value);
            }

            return cars;
        }

        private IQueryable<Car> SearchCarsByBusiness(SearchFilterDto.CarBusinessSearchDto carBusiness, IQueryable<Car> cars)
        {
            if (!String.IsNullOrEmpty(carBusiness.Location))
            {
                cars = cars.Where(x => x.CarBusiness.Location.Contains(carBusiness.Location));
            }

            if (carBusiness.UserId.HasValue)
            {
                cars = cars.Where(x => x.CarBusiness.UserId.HasValue
                                    && x.CarBusiness.UserId.Value == carBusiness.UserId.Value);
            }

            if (carBusiness.CreateStartDate.HasValue && carBusiness.CreateEndDate.HasValue)
            {
                cars = cars.Where(x => carBusiness.CreateStartDate.Value <= x.CarBusiness.CreateDate
                                    && x.CarBusiness.CreateDate <= carBusiness.CreateEndDate.Value);
            }
            else if (carBusiness.CreateStartDate.HasValue)
            {
                cars = cars.Where(x => carBusiness.CreateStartDate.Value <= x.CarBusiness.CreateDate);
            }
            else if (carBusiness.CreateEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarBusiness.CreateDate <= carBusiness.CreateEndDate.Value);
            }

            if (carBusiness.UpdateStartDate.HasValue && carBusiness.UpdateEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                    && carBusiness.UpdateStartDate.Value <= x.CarBusiness.UpdateDate.Value
                                    && x.CarBusiness.UpdateDate.Value <= carBusiness.UpdateEndDate.Value);
            }
            else if (carBusiness.UpdateStartDate.HasValue)
            {
                cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                    && carBusiness.UpdateStartDate.Value <= x.CarBusiness.UpdateDate.Value);
            }
            else if (carBusiness.UpdateEndDate.HasValue)
            {
                cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                    && x.CarBusiness.UpdateDate.Value <= carBusiness.UpdateEndDate.Value);
            }

            return cars;
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
            car.Decommissioned = car.Decommissioned;
            car.Color = car.Color;
            car.ChassisNumber = car.ChassisNumber;

            car.CarSpec.Catalyst = carDto.CarSpec.Catalyst;
            car.CarSpec.EngineCode = carDto.CarSpec.EngineCode;
            car.CarSpec.EngineDisplacement = carDto.CarSpec.EngineDisplacement;
            car.CarSpec.EngineOilId = car.CarSpec.EngineOilId;
            car.CarSpec.FuelId = carDto.CarSpec.FuelId;
            car.CarSpec.GearOilId = carDto.CarSpec.GearOilId;
            car.CarSpec.HybridDrive = carDto.CarSpec.HybridDrive;
            car.CarSpec.MaxSpeed = carDto.CarSpec.MaxSpeed;
            car.CarSpec.Performance = carDto.CarSpec.Performance;
            car.CarSpec.ProductionDate = carDto.CarSpec.ProductionDate;
            car.CarSpec.RegistrationDate = carDto.CarSpec.RegistrationDate;
            car.CarSpec.TotalWeight = carDto.CarSpec.TotalWeight;

            car.CarBusiness.Location = carDto.CarBusiness.Location;
            car.CarBusiness.CreateDate = carDto.CarBusiness.CreateDate;
            car.CarBusiness.UpdateDate = carDto.CarBusiness.UpdateDate;
            car.CarBusiness.UserId = carDto.CarBusiness.UserId;

            await carRepository.UpdateCar(car);
        }
    }
}
