using Anthill.Common.Data;
using Fuhrpark.Data.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Data
{
    public class FuhrparkConnectionConfiguration: AbstractConnectionConfiguration, IFuhrparkConnectionConfiguration
    {
        public FuhrparkConnectionConfiguration(IConfigurationRoot configurationRoot, string connectionName)
            : base(configurationRoot, connectionName)
        {
        }
    }
}
