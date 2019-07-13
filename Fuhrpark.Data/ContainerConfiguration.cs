using Fuhrpark.Data.Contracts;
using Fuhrpark.Data.Contracts.Repositories;
using Fuhrpark.Data.Repositories;
using Fuhrpark.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Fuhrpark.Data
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
          where TLifetime : LifetimeManager, new()
        {
            var configuration = container.Resolve<IConfiguration>();

            container.RegisterType<IFuhrparkConnectionConfiguration, FuhrparkConnectionConfiguration>(
               new TLifetime(),
               new InjectionConstructor(configuration, "DefaultConnection"));

            container.RegisterType<FuhrparkDataContext>();
            container.RegisterType<IFuhrparkDataContextManager, FuhrparkDataContextManager>(new TLifetime());

            container.RegisterType<IAccountRepository, AccountRepository>(new TLifetime());
            container.RegisterType<ICarRepository, CarRepository>(new TLifetime());

            container.RegisterType<ICommonRepository<Manufacturer>, CommonRepository<Manufacturer>>(new TLifetime());
            container.RegisterType<ICommonRepository<Typ>, CommonRepository<Typ>>(new TLifetime());
            container.RegisterType<ICommonRepository<Fuel>, CommonRepository<Fuel>>(new TLifetime());
            container.RegisterType<ICommonRepository<EngineOil>, CommonRepository<EngineOil>>(new TLifetime());
            container.RegisterType<ICommonRepository<GearOil>, CommonRepository<GearOil>>(new TLifetime());
            container.RegisterType<ICommonRepository<User>, CommonRepository<User>>(new TLifetime());
        }
    }
}
