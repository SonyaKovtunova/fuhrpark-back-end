using Anthill.Common.Services;
using AutoMapper;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos.Common;
using Fuhrpark.Services.Contracts.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Mappers
{
    public class CommonAddMap<TEntity, TAddDto> : AbstractMapper<TEntity, TAddDto>, ICommonAddMap<TEntity, TAddDto> 
        where TEntity : CommonEntity where TAddDto : CommonAddDto
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAddDto, TEntity>();
            });


            return config.CreateMapper();
        }
    }
}
