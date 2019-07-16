using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetCars(SearchFilterDto searchDto);

        Task AddCar(CarAddDto car);

        Task UpdateCar(CarUpdateDto car);

        Task<CarRemoveInfoDto> RemoveCar(RemoveCarSettingsDto removeCarSettings);

        Task<CarDto> GetCarById(int id);
    }
}
