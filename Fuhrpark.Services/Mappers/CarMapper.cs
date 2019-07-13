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
    public class CarMapper : AbstractMapper<Car, CarDto>, ICarMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Typ, TypDto>();
                cfg.CreateMap<Manufacturer, ManufacturerDto>();
                cfg.CreateMap<Fuel, FuelDto>();
                cfg.CreateMap<EngineOil, EngineOilDto>();
                cfg.CreateMap<GearOil, GearOilDto>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<CarSpec, CarDto.CarSpecDto>();
                cfg.CreateMap<CarBusiness, CarDto.CarBusinessDto>();
                cfg.CreateMap<Car, CarDto>();

                cfg.CreateMap<TypDto, Typ>();
                cfg.CreateMap<ManufacturerDto, Manufacturer>();
                cfg.CreateMap<FuelDto, Fuel>();
                cfg.CreateMap<EngineOilDto, EngineOil>();
                cfg.CreateMap<GearOilDto, GearOil>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<CarDto.CarSpecDto, CarSpec>();
                cfg.CreateMap<CarDto.CarBusinessDto, CarBusiness>();
                cfg.CreateMap<CarDto, Car>();
            });


            return config.CreateMapper();
        }
    }
}
