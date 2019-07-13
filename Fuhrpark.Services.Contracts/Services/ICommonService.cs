using Fuhrpark.Enums;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICommonService<Dto> where Dto: CommonDto
    {
        Task<IEnumerable<Dto>> GetAll();

        Task Add(Dto dto);

        Task Update(Dto dto);

        Task Remove(int id, RemovalType removalType);
    }
}
