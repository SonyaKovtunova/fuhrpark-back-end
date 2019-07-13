using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface ICarSearchModelMapper : IModelMapper<CarSearchDto, CarSearchModel>
    {
    }
    public class CarSearchModelMapper : AbstractModelMapper<CarSearchDto, CarSearchModel>, ICarSearchModelMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarSearchModel.CarSpecSearchModel, CarSearchDto.CarSpecSearchDto>();
                cfg.CreateMap<CarSearchModel.CarBusinessSearchModel, CarSearchDto.CarBusinessSearchDto>();
                cfg.CreateMap<CarSearchModel, CarSearchDto>();
            });


            return config.CreateMapper();
        }
    }
}
