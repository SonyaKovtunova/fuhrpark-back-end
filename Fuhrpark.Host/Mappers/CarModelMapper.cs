using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface ICarModelMapper : IModelMapper<CarDto, CarModel>
    {
    }

    public class CarModelMapper : AbstractModelMapper<CarDto, CarModel>, ICarModelMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypDto, TypModel>();
                cfg.CreateMap<ManufacturerDto, ManufacturerModel>();
                cfg.CreateMap<CarDto, CarModel>();
            });

            return config.CreateMapper();
        }
    }
}
