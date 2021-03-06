﻿using Anthill.Common.Services;
using AutoMapper;
using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuhrpark.Services.Mappers
{
    public class CarSubgroupMapper : AbstractMapper<CarSubgroup, CarSubgroupDto>, ICarSubgroupMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Typ, TypDto>();
                cfg.CreateMap<Manufacturer, ManufacturerDto>();
                cfg.CreateMap<User, UserDto>();

                cfg.CreateMap<Car, CarSubgroupDto.CarInfo>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.RegistrationNumber, y => y.MapFrom(z => z.RegistrationNumber))
                    .ForMember(x => x.Model, y => y.MapFrom(z => z.Model))
                    .ForMember(x => x.Typ, y => y.MapFrom(z => z.Typ))
                    .ForMember(x => x.Manufacturer, y => y.MapFrom(z => z.Manufacturer))
                    .ForMember(x => x.User, y => y.MapFrom(z => z.CarBusiness.User));

                cfg.CreateMap<CarSubgroup, CarSubgroupDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                    .ForMember(x => x.Cars, y => y.MapFrom(z => z.CarInCarSubgroups.Select(s => s.Car).ToList()));
            });


            return config.CreateMapper();
        }
    }
}
