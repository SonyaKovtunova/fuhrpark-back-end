﻿using Anthill.Common.Services.Contracts;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Mappers
{
    public interface ICommonMapper<TEntity, Dto> : IMapper<TEntity, Dto> where TEntity: CommonEntity where Dto: CommonDto
    {
    }
}
