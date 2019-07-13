using AutoMapper;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Mappers
{
    public interface ICarDetailModelMapper : IModelMapper<CarDto, CarDetailModel>
    {
    }

    public class CarDetailModelMapper : AbstractModelMapper<CarDto, CarDetailModel>, ICarDetailModelMapper
    {
        protected override IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypDto, TypModel>();
                cfg.CreateMap<ManufacturerDto, ManufacturerModel>();
                cfg.CreateMap<FuelDto, FuelModel>();
                cfg.CreateMap<EngineOilDto, EngineOilModel>();
                cfg.CreateMap<GearOilDto, GearOilModel>();
                cfg.CreateMap<UserDto, UserModel>();
                cfg.CreateMap<CarDto.CarSpecDto, CarDetailModel.CarSpecModel>();
                cfg.CreateMap<CarDto.CarBusinessDto, CarDetailModel.CarBusinessModel>();
                cfg.CreateMap<CarDto, CarDetailModel>();

                cfg.CreateMap<TypModel, TypDto>();
                cfg.CreateMap<ManufacturerModel, ManufacturerDto>();
                cfg.CreateMap<FuelModel, FuelDto>();
                cfg.CreateMap<EngineOilModel, EngineOilDto>();
                cfg.CreateMap<GearOilModel, GearOilDto>();
                cfg.CreateMap<UserModel, UserDto>();
                cfg.CreateMap<CarDetailModel.CarSpecModel, CarDto.CarSpecDto>();
                cfg.CreateMap<CarDetailModel.CarBusinessModel, CarDto.CarBusinessDto>();
                cfg.CreateMap<CarDetailModel, CarDto>();
            });


            return config.CreateMapper();
        }
    }
}
