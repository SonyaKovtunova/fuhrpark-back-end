using Anthill.Common.Data.Contracts;
using Fuhrpark.Enums;
using Fuhrpark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Contracts.Repositories
{
    public interface ICarRepository: IRepository
    {
        IQueryable<Car> GetCars();

        Task<Car> GetCarById(int id);

        Task AddCar(Car car);

        Task UpdateCar(Car car);

        Task RemoveCar(Car car);

        Task<Car> GetCarByManufacturerId(int manufacturerId, int carId);

        Task<Car> GetCarByTypId(int typId, int carId);

        Task<Car> GetCarByFuelId(int fuelId, int carId);

        Task<Car> GetCarByEngineOilId(int engineOilId, int carId);

        Task<Car> GetCarByGearOilId(int gearOilId, int carId);

        Task<Car> GetCarByUserId(int userId, int carId);

        Task<Car> GetCarByEntityType(int id, RemovalType removalType);

        Task<IEnumerable<Car>> GetCarsByIds(IEnumerable<int> carIds);

        Task<IEnumerable<string>> GetCarRegistrationNumbers();
    }
}
