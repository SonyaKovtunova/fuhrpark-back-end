using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fuhrpark.Data
{
    public class FuhrparkDataContextFactory : IDesignTimeDbContextFactory<FuhrparkDataContext>
    {
        public FuhrparkDataContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var config = new FuhrparkConnectionConfiguration(configuration, "DefaultConnection");
            return new FuhrparkDataContext(config);
        }
    }
}
