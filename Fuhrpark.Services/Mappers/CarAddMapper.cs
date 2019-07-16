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
    public class CarAddMapper : AbstractMapper<Car, CarAddDto>, ICarAddMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarAddDto.CarSpecAddDto, CarSpec>();
                cfg.CreateMap<CarAddDto.CarBusinessAddDto, CarBusiness>();
                cfg.CreateMap<CarAddDto, Car>();
            });


            return config.CreateMapper();
        }
    }
}
