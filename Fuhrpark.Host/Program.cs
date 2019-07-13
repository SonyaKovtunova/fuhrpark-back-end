using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace Fuhrpark.Host
{
    public class Program
    {
        private static IUnityContainer _container;

        public static void Main(string[] args)
        {
            _container = new UnityContainer();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUnityServiceProvider(_container)
                .UseStartup<Startup>();
    }
}
