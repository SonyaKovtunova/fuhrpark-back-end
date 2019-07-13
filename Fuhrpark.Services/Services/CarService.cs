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

        public async Task AddCar(CarDto carDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = MapperFactory.CreateMapper<ICarMapper>().MapFromModel(carDto);

            await carRepository.AddCar(car);
            await DataContextManager.SaveAsync();
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

        public async Task<IEnumerable<CarDto>> GetCars(CarSearchDto searchDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var cars = carRepository.GetCars();

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

            if (searchDto.CarSpec != null)
            {
                if (searchDto.CarSpec.Catalyst.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.Catalyst == searchDto.CarSpec.Catalyst.Value);
                }

                if (!String.IsNullOrEmpty(searchDto.CarSpec.EngineCode))
                {
                    cars = cars.Where(x => x.CarSpec.EngineCode.Contains(searchDto.CarSpec.EngineCode));
                }

                if (searchDto.CarSpec.EngineOilId.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.EngineOilId == searchDto.CarSpec.EngineOilId.Value);
                }

                if (searchDto.CarSpec.FuelId.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.FuelId == searchDto.CarSpec.FuelId.Value);
                }

                if (searchDto.CarSpec.GearOilId.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.GearOilId == searchDto.CarSpec.GearOilId.Value);
                }

                if (searchDto.CarSpec.HybridDrive.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.HybridDrive == searchDto.CarSpec.HybridDrive.Value);
                }

                if (searchDto.CarSpec.MaxEngineDisplacement.HasValue && searchDto.CarSpec.MinEngineDisplacement.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue 
                            && searchDto.CarSpec.MinEngineDisplacement.Value <= x.CarSpec.EngineDisplacement.Value 
                            && x.CarSpec.EngineDisplacement.Value <= searchDto.CarSpec.MaxEngineDisplacement.Value);
                }
                else if (searchDto.CarSpec.MaxEngineDisplacement.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue
                            && x.CarSpec.EngineDisplacement.Value <= searchDto.CarSpec.MaxEngineDisplacement.Value);
                }
                else if (searchDto.CarSpec.MinEngineDisplacement.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.EngineDisplacement.HasValue
                            && searchDto.CarSpec.MinEngineDisplacement.Value <= x.CarSpec.EngineDisplacement.Value);
                }

                if (searchDto.CarSpec.MaxPerformance.HasValue && searchDto.CarSpec.MinPerformance.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.Performance.HasValue
                            && searchDto.CarSpec.MinPerformance.Value <= x.CarSpec.Performance.Value
                            && x.CarSpec.Performance.Value <= searchDto.CarSpec.MaxPerformance.Value);
                }
                else if (searchDto.CarSpec.MaxPerformance.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.Performance.HasValue
                            && x.CarSpec.Performance.Value <= searchDto.CarSpec.MaxPerformance.Value);
                }
                else if (searchDto.CarSpec.MinPerformance.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.Performance.HasValue
                            && searchDto.CarSpec.MinPerformance.Value <= x.CarSpec.Performance.Value);
                }

                if (searchDto.CarSpec.MaxSpeed.HasValue && searchDto.CarSpec.MinSpeed.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                            && searchDto.CarSpec.MinSpeed.Value <= x.CarSpec.MaxSpeed.Value
                            && x.CarSpec.MaxSpeed.Value <= searchDto.CarSpec.MaxSpeed.Value);
                }
                else if (searchDto.CarSpec.MaxSpeed.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                            && x.CarSpec.MaxSpeed.Value <= searchDto.CarSpec.MaxSpeed.Value);
                }
                else if (searchDto.CarSpec.MinSpeed.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.MaxSpeed.HasValue
                            && searchDto.CarSpec.MinSpeed.Value <= x.CarSpec.MaxSpeed.Value);
                }

                if (searchDto.CarSpec.MaxTotalWeight.HasValue && searchDto.CarSpec.MinTotalWeight.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                            && searchDto.CarSpec.MinTotalWeight.Value <= x.CarSpec.TotalWeight.Value
                            && x.CarSpec.TotalWeight.Value <= searchDto.CarSpec.MaxTotalWeight.Value);
                }
                else if (searchDto.CarSpec.MaxTotalWeight.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                            && x.CarSpec.TotalWeight.Value <= searchDto.CarSpec.MaxTotalWeight.Value);
                }
                else if (searchDto.CarSpec.MinTotalWeight.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.TotalWeight.HasValue
                            && searchDto.CarSpec.MinTotalWeight.Value <= x.CarSpec.TotalWeight.Value);
                }

                if (searchDto.CarSpec.ProductionStartDate.HasValue && searchDto.CarSpec.ProductionEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                    && searchDto.CarSpec.ProductionStartDate.Value <= x.CarSpec.ProductionDate.Value
                                    && x.CarSpec.ProductionDate.Value <= searchDto.CarSpec.ProductionEndDate.Value);
                }
                else if (searchDto.CarSpec.ProductionStartDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                    && searchDto.CarSpec.ProductionStartDate.Value <= x.CarSpec.ProductionDate.Value);
                }
                else if (searchDto.CarSpec.ProductionEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.ProductionDate.HasValue
                                    && x.CarSpec.ProductionDate.Value <= searchDto.CarSpec.ProductionEndDate.Value);
                }

                if (searchDto.CarSpec.RegistrationStartDate.HasValue && searchDto.CarSpec.RegistrationEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                    && searchDto.CarSpec.RegistrationStartDate.Value <= x.CarSpec.RegistrationDate.Value
                                    && x.CarSpec.RegistrationDate.Value <= searchDto.CarSpec.RegistrationEndDate.Value);
                }
                else if (searchDto.CarSpec.RegistrationStartDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                    && searchDto.CarSpec.RegistrationStartDate.Value <= x.CarSpec.RegistrationDate.Value);
                }
                else if (searchDto.CarSpec.RegistrationEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarSpec.RegistrationDate.HasValue
                                    && x.CarSpec.RegistrationDate.Value <= searchDto.CarSpec.RegistrationEndDate.Value);
                }
            }

            if (searchDto.CarBusiness != null)
            {
                if (!String.IsNullOrEmpty(searchDto.CarBusiness.Location))
                {
                    cars = cars.Where(x => x.CarBusiness.Location.Contains(searchDto.CarBusiness.Location));
                }

                if (searchDto.CarBusiness.UserId.HasValue)
                {
                    cars = cars.Where(x => x.CarBusiness.UserId.HasValue
                                        && x.CarBusiness.UserId.Value == searchDto.CarBusiness.UserId.Value);
                }

                if (searchDto.CarBusiness.CreateStartDate.HasValue && searchDto.CarBusiness.CreateEndDate.HasValue)
                {
                    cars = cars.Where(x => searchDto.CarBusiness.CreateStartDate.Value <= x.CarBusiness.CreateDate
                                        && x.CarBusiness.CreateDate <= searchDto.CarBusiness.CreateEndDate.Value);
                }
                else if (searchDto.CarBusiness.CreateStartDate.HasValue)
                {
                    cars = cars.Where(x => searchDto.CarBusiness.CreateStartDate.Value <= x.CarBusiness.CreateDate);
                }
                else if (searchDto.CarBusiness.CreateEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarBusiness.CreateDate <= searchDto.CarBusiness.CreateEndDate.Value);
                }

                if (searchDto.CarBusiness.UpdateStartDate.HasValue && searchDto.CarBusiness.UpdateEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                        && searchDto.CarBusiness.UpdateStartDate.Value <= x.CarBusiness.UpdateDate.Value
                                        && x.CarBusiness.UpdateDate.Value <= searchDto.CarBusiness.UpdateEndDate.Value);
                }
                else if (searchDto.CarBusiness.UpdateStartDate.HasValue)
                {
                    cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                        && searchDto.CarBusiness.UpdateStartDate.Value <= x.CarBusiness.UpdateDate.Value);
                }
                else if (searchDto.CarBusiness.UpdateEndDate.HasValue)
                {
                    cars = cars.Where(x => x.CarBusiness.UpdateDate.HasValue
                                        && x.CarBusiness.UpdateDate.Value <= searchDto.CarBusiness.UpdateEndDate.Value);
                }
            }

            var foundCars = await cars.ToListAsync();

            return MapperFactory.CreateMapper<ICarMapper>().MapCollectionToModel(foundCars);
        }

        public async Task<CarRemoveInfoDto> RemoveCar(int id, bool isCheck, bool manufacturerIsDelete, bool typIsDelete, bool fuelIsDelete,
            bool engineOilIsDelete, bool gearOilIsDelete, bool userIsDelete)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarById(id);

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            var carWithSameManufacturer = await carRepository.GetCarByManufacturerId(car.ManufacturerId, id);
            var carWithSameTyp = await carRepository.GetCarByTypId(car.TypId, id);
            var carWithSameFuel = await carRepository.GetCarByFuelId(car.CarSpec.FuelId, id);
            var carWithSameEngineOil = await carRepository.GetCarByEngineOilId(car.CarSpec.EngineOilId, id);
            var carWithSameGearOil = await carRepository.GetCarByGearOilId(car.CarSpec.GearOilId, id);
            Car carWithSameUser = null;

            if (car.CarBusiness.UserId.HasValue)
            {
                carWithSameUser = await carRepository.GetCarByUserId(car.CarBusiness.UserId.Value, id);
            }

            if (!isCheck)
            {
                carRepository.RemoveCar(car);

                if (carWithSameManufacturer == null && manufacturerIsDelete && car.Manufacturer != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<Manufacturer>>();
                    repository.Remove(car.Manufacturer);
                }

                if (carWithSameTyp == null && typIsDelete && car.Typ != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<Typ>>();
                    repository.Remove(car.Typ);
                }

                if (carWithSameFuel == null && fuelIsDelete && car.CarSpec.Fuel != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<Fuel>>();
                    repository.Remove(car.CarSpec.Fuel);
                }

                if (carWithSameGearOil == null && gearOilIsDelete && car.CarSpec.GearOil != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<GearOil>>();
                    repository.Remove(car.CarSpec.GearOil);
                }

                if (carWithSameEngineOil == null && engineOilIsDelete && car.CarSpec.EngineOil != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<EngineOil>>();
                    repository.Remove(car.CarSpec.EngineOil);
                }

                if (carWithSameUser == null && userIsDelete && car.CarBusiness.User != null)
                {
                    var repository = DataContextManager.CreateRepository<ICommonRepository<User>>();
                    repository.Remove(car.CarBusiness.User);
                }

                await DataContextManager.SaveAsync();
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

        public async Task UpdateCar(CarDto carDto)
        {
            var carRepository = DataContextManager.CreateRepository<ICarRepository>();

            var car = await carRepository.GetCarById(carDto.Id);

            if (car == null)
            {
                throw new ObjectNotFoundException();
            }

            car.Typ = null;
            car.TypId = carDto.TypId;
            car.Manufacturer = null;
            car.ManufacturerId = carDto.ManufacturerId;
            car.RegistrationNumber = carDto.RegistrationNumber;
            car.Model = carDto.Model;
            car.Decommissioned = car.Decommissioned;
            car.Color = car.Color;
            car.ChassisNumber = car.ChassisNumber;

            car.CarSpec.Catalyst = carDto.CarSpec.Catalyst;
            car.CarSpec.EngineCode = carDto.CarSpec.EngineCode;
            car.CarSpec.EngineDisplacement = carDto.CarSpec.EngineDisplacement;
            car.CarSpec.EngineOil = null;
            car.CarSpec.EngineOilId = car.CarSpec.EngineOilId;
            car.CarSpec.Fuel = null;
            car.CarSpec.FuelId = carDto.CarSpec.FuelId;
            car.CarSpec.GearOil = null;
            car.CarSpec.GearOilId = carDto.CarSpec.GearOilId;
            car.CarSpec.HybridDrive = carDto.CarSpec.HybridDrive;
            car.CarSpec.MaxSpeed = carDto.CarSpec.MaxSpeed;
            car.CarSpec.Performance = carDto.CarSpec.Performance;
            car.CarSpec.ProductionDate = carDto.CarSpec.ProductionDate;
            car.CarSpec.RegistrationDate = carDto.CarSpec.RegistrationDate;
            car.CarSpec.TotalWeight = carDto.CarSpec.TotalWeight;

            car.CarBusiness.CreateDate = carDto.CarBusiness.CreateDate;
            car.CarBusiness.Location = carDto.CarBusiness.Location;
            car.CarBusiness.UpdateDate = carDto.CarBusiness.UpdateDate;
            car.CarBusiness.User = null;
            car.CarBusiness.UserId = carDto.CarBusiness.UserId;

            carRepository.UpdateCar(car);
            await DataContextManager.SaveAsync();
        }
    }
}
