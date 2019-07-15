using Anthill.Common.Services.Contracts;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Mappers
{
    public interface ICommonAddMap<TEntity, TAddDto> : IMapper<TEntity, TAddDto> where TEntity : CommonEntity where TAddDto: CommonAddDto
    {
    }
}
