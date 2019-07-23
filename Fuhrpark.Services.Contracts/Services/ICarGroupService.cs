using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICarGroupService
    {
        Task<IEnumerable<CarGroupDto>> GetAll();

        Task Add(CarGroupAddDto carGroupDto);

        Task Update(CarGroupUpdateDto carGroupDto);
    }
}
