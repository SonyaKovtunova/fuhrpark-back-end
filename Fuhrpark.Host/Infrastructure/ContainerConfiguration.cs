using Fuhrpark.Host.Controllers;
using Fuhrpark.Host.Mappers;
using Fuhrpark.Host.Models;
using Fuhrpark.Services.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace Fuhrpark.Host.Infrastructure
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
           where TLifetime : LifetimeManager, new()
        {
            Services.ContainerConfiguration.RegisterTypes<TLifetime>(container);

            container.RegisterType<AccountController>();
            container.RegisterType<CarController>();
            container.RegisterType<ManufacturerController>();
            container.RegisterType<TypController>();
            container.RegisterType<FuelController>();
            container.RegisterType<EngineOilController>();
            container.RegisterType<GearOilController>();
            container.RegisterType<UserController>();

            container.RegisterType<IModelMapperFactory, ModelMapperFactory>(new TLifetime());

            container.RegisterType<IUserRegisterMapper, UserRegisterMapper>(new TLifetime());
            container.RegisterType<ICarModelMapper, CarModelMapper>(new TLifetime());
            container.RegisterType<ICarDetailModelMapper, CarDetailModelMapper>(new TLifetime());
            container.RegisterType<ICarAddModelMapper, CarAddModelMapper>(new TLifetime());
            container.RegisterType<ICarUpdateModelMapper, CarUpdateModelMapper>(new TLifetime());
            container.RegisterType<ICarSearchModelMapper, CarSearchModelMapper>(new TLifetime());
            container.RegisterType<ICommonModelMapper<ManufacturerDto, ManufacturerModel>, CommonModelMapper<ManufacturerDto, ManufacturerModel>>(new TLifetime());
            container.RegisterType<ICommonModelMapper<TypDto, TypModel>, CommonModelMapper<TypDto, TypModel>>(new TLifetime());
            container.RegisterType<ICommonModelMapper<FuelDto, FuelModel>, CommonModelMapper<FuelDto, FuelModel>>(new TLifetime());
            container.RegisterType<ICommonModelMapper<EngineOilDto, EngineOilModel>, CommonModelMapper<EngineOilDto, EngineOilModel>>(new TLifetime());
            container.RegisterType<ICommonModelMapper<GearOilDto, GearOilModel>, CommonModelMapper<GearOilDto, GearOilModel>>(new TLifetime());
            container.RegisterType<ICommonModelMapper<UserDto, UserModel>, CommonModelMapper<UserDto, UserModel>>(new TLifetime());
        }
    }
}
