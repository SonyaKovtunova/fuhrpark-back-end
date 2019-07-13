using Anthill.Common.Services;
using AutoMapper;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Mappers
{
    public class CommonMapper<TEntity, Dto> : AbstractMapper<TEntity, Dto>, ICommonMapper<TEntity, Dto> where TEntity : CommonEntity where Dto : CommonDto
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntity, Dto>();
                cfg.CreateMap<Dto, TEntity>();
            });


            return config.CreateMapper();
        }
    }
}
