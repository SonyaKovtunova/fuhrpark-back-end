﻿using Fuhrpark.Models;
using Fuhrpark.Services.Contracts.Dtos;
using Fuhrpark.Services.Contracts.Dtos.Common;
using Fuhrpark.Services.Contracts.Mappers;
using Fuhrpark.Services.Contracts.Services;
using Fuhrpark.Services.Mappers;
using Fuhrpark.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Lifetime;

namespace Fuhrpark.Services
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
           where TLifetime : LifetimeManager, new()
        {
            Data.ContainerConfiguration.RegisterTypes<TLifetime>(container);

            container.RegisterType<IAccountService, AccountService>(new TLifetime());
            container.RegisterType<ICarService, CarService>(new TLifetime());
            container.RegisterType<ICarSubgroupService, CarSubgroupService>(new TLifetime());
            container.RegisterType<ICarGroupService, CarGroupService>(new TLifetime());
            container.RegisterType<ICommonService<ManufacturerDto>, CommonService<ManufacturerDto, Manufacturer>>(new TLifetime());
            container.RegisterType<ICommonService<TypDto>, CommonService<TypDto, Typ>>(new TLifetime());
            container.RegisterType<ICommonService<FuelDto>, CommonService<FuelDto, Fuel>>(new TLifetime());
            container.RegisterType<ICommonService<EngineOilDto>, CommonService<EngineOilDto, EngineOil>>(new TLifetime());
            container.RegisterType<ICommonService<GearOilDto>, CommonService<GearOilDto, GearOil>>(new TLifetime());
            container.RegisterType<ICommonService<UserDto>, CommonService<UserDto, User>>(new TLifetime());


            container.RegisterType<IAppUserMapper, AppUserMapper>(new TLifetime());
            container.RegisterType<ICarMapper, CarMapper>(new TLifetime());
            container.RegisterType<ICarSubgroupMapper, CarSubgroupMapper>(new TLifetime());
            container.RegisterType<ICarGroupMapper, CarGroupMapper>(new TLifetime());
            container.RegisterType<ICommonMapper<Manufacturer, ManufacturerDto>, CommonMapper<Manufacturer, ManufacturerDto>>(new TLifetime());
            container.RegisterType<ICommonMapper<Typ, TypDto>, CommonMapper<Typ, TypDto>>(new TLifetime());
            container.RegisterType<ICommonMapper<Fuel, FuelDto>, CommonMapper<Fuel, FuelDto>>(new TLifetime());
            container.RegisterType<ICommonMapper<EngineOil, EngineOilDto>, CommonMapper<EngineOil, EngineOilDto>>(new TLifetime());
            container.RegisterType<ICommonMapper<GearOil, GearOilDto>, CommonMapper<GearOil, GearOilDto>>(new TLifetime());
            container.RegisterType<ICommonMapper<User, UserDto>, CommonMapper<User, UserDto>>(new TLifetime());

            container.RegisterType<ICommonAddMap<Manufacturer, CommonAddDto>, CommonAddMap<Manufacturer, CommonAddDto>>(new TLifetime());
            container.RegisterType<ICommonAddMap<Typ, CommonAddDto>, CommonAddMap<Typ, CommonAddDto>>(new TLifetime());
            container.RegisterType<ICommonAddMap<Fuel, CommonAddDto>, CommonAddMap<Fuel, CommonAddDto>>(new TLifetime());
            container.RegisterType<ICommonAddMap<EngineOil, CommonAddDto>, CommonAddMap<EngineOil, CommonAddDto>>(new TLifetime());
            container.RegisterType<ICommonAddMap<GearOil, CommonAddDto>, CommonAddMap<GearOil, CommonAddDto>>(new TLifetime());
            container.RegisterType<ICommonAddMap<User, CommonAddDto>, CommonAddMap<User, CommonAddDto>>(new TLifetime());

            container.RegisterType<ICarAddMapper, CarAddMapper>(new TLifetime());
        }
    }
}
