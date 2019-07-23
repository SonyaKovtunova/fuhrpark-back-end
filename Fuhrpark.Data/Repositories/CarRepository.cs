using Anthill.Common.Data;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Enums;
using Fuhrpark.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Data.Repositories
{
    public class CarRepository : AbstractRepository, ICarRepository
    {
        public CarRepository(FuhrparkDataContext context) : base(context)
        {
        }

        public async Task AddCar(Car car)
        {
            await Context.Set<Car>()
                .AddAsync(car);

            await Context.SaveChangesAsync();
        }

        public async Task<Car> GetCarByEngineOilId(int engineOilId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.CarSpec.EngineOilId == engineOilId && x.Id != carId);
        }

        public async Task<Car> GetCarByEntityType(int id, RemovalType removalType)
        {
            switch (removalType)
            {
                case RemovalType.Manufacturer:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.ManufacturerId == id);
                case RemovalType.Typ:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.TypId == id);
                case RemovalType.Fuel:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.CarSpec.FuelId == id);
                case RemovalType.EngineOil:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.CarSpec.EngineOilId == id);
                case RemovalType.GearOil:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.CarSpec.GearOilId == id);
                case RemovalType.User:
                    return await Context.Set<Car>().FirstOrDefaultAsync(x => x.CarBusiness.UserId.HasValue && x.CarBusiness.UserId.Value == id);
            };

            return null;
        }

        public async Task<Car> GetCarByFuelId(int fuelId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.CarSpec.FuelId == fuelId && x.Id != carId);
        }

        public async Task<Car> GetCarByGearOilId(int gearOilId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.CarSpec.GearOilId == gearOilId && x.Id != carId);
        }

        public async Task<Car> GetCarById(int id)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Car> GetCarByManufacturerId(int manufacturerId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.ManufacturerId == manufacturerId && x.Id != carId);
        }

        public async Task<Car> GetCarByTypId(int typId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.TypId == typId && x.Id != carId);
        }

        public async Task<Car> GetCarByUserId(int userId, int carId)
        {
            return await Context.Set<Car>()
                .FirstOrDefaultAsync(x => x.CarBusiness.UserId == userId && x.Id != carId);
        }

        public IQueryable<Car> GetCars()
        {
            return Context.Set<Car>()
                .AsQueryable();
        }

        public async Task<IEnumerable<Car>> GetCarsByIds(IEnumerable<int> carIds)
        {
            return await Context.Set<Car>()
                .Where(x => carIds.Contains(x.Id))
                .ToListAsync();
        }

        public async Task RemoveCar(Car car)
        {
            Context.Set<Car>()
                .Remove(car);

            await Context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            Context.Entry<Car>(car).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
    }
}
