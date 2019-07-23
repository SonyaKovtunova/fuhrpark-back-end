using Anthill.Common.Services;
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
    public class CarGroupMapper : AbstractMapper<CarGroup, CarGroupDto>, ICarGroupMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Typ, TypDto>();
                cfg.CreateMap<Manufacturer, ManufacturerDto>();

                cfg.CreateMap<Car, CarSubgroupDto.CarInfo>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.RegistrationNumber, y => y.MapFrom(z => z.RegistrationNumber))
                    .ForMember(x => x.Model, y => y.MapFrom(z => z.Model));

                cfg.CreateMap<CarSubgroup, CarSubgroupDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                    .ForMember(x => x.Cars, y => y.MapFrom(z => z.CarInCarSubgroups.Select(s => s.Car).ToList()));

                cfg.CreateMap<CarGroup, CarGroupDto>()
                    .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForMember(x => x.Name, y => y.MapFrom(z => z.Name))
                    .ForMember(x => x.CarSubgroups, y => y.MapFrom(z => z.CarSubgroupInCarGroups.Select(s => s.CarSubgroup).ToList()));
            });


            return config.CreateMapper();
        }
    }
}
