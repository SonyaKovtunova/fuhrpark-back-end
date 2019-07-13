using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetCars(CarSearchDto searchDto);

        Task AddCar(CarDto car);

        Task UpdateCar(CarDto car);

        Task<CarRemoveInfoDto> RemoveCar(int id, bool isCheck, bool manufacturerIsDelete, bool typIsDelete, bool fuelIsDelete,
            bool engineOilIsDelete, bool gearOilIsDelete, bool userIsDelete);

        Task<CarDto> GetCarById(int id);
    }
}
