using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface ICarAddModelMapper : IModelMapper<CarDto, CarAddModel>
    {
    }

    public class CarAddModelMapper : AbstractModelMapper<CarDto, CarAddModel>, ICarAddModelMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarSpecModel, CarDto.CarSpecDto>();
                cfg.CreateMap<CarBusinessModel, CarDto.CarBusinessDto>();
                cfg.CreateMap<CarAddModel, CarDto>();
            });


            return config.CreateMapper();
        }
    }
}
