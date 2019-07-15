using Fuhrpark.Enums;
using Fuhrpark.Services.Contracts.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fuhrpark.Services.Contracts.Services
{
    public interface ICommonService<Dto> where Dto: CommonDto
    {
        Task<IEnumerable<Dto>> GetAll();

        Task Add(CommonAddDto dto);

        Task Update(CommonUpdateDto dto);

        Task Remove(int id, RemovalType removalType);
    }
}
