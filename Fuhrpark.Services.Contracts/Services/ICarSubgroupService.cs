using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICarSubgroupService
    {
        Task<IEnumerable<CarSubgroupDto>> GetAll();

        Task Add(CarSubgroupAddDto carSubgroupDto);

        Task Update(CarSubgroupUpdateDto carSubgroupDto);
    }
}
