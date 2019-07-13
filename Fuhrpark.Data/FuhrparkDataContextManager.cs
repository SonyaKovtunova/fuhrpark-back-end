using Anthill.Common.Data;
using Fuhrpark.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace Fuhrpark.Data
{
    public class FuhrparkDataContextManager : AbstractDataContextManager<FuhrparkDataContext>, IFuhrparkDataContextManager
    {
        public FuhrparkDataContextManager(IUnityContainer container) : base(container)
        {
        }
    }
}
