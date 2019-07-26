using Fuhrpark.Host.Controllers;
using Fuhrpark.Host.Helpers;
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

            container.RegisterType<IEmailSender, EmailSender>();

            container.RegisterType<AccountController>();
            container.RegisterType<CarController>();
            container.RegisterType<CarSubgroupController>();
            container.RegisterType<CarGroupController>();
            container.RegisterType<ManufacturerController>();
            container.RegisterType<TypController>();
            container.RegisterType<FuelController>();
            container.RegisterType<EngineOilController>();
            container.RegisterType<GearOilController>();
            container.RegisterType<UserController>();
        }
    }
}
