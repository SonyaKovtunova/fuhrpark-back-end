using Anthill.Common.Services.Contracts;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Mappers
{
    public interface IAppUserMapper : IMapper<AppUser, AppUserDto>
    {
    }
}
