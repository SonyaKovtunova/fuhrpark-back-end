using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Exceptions
{
    public class ObjectNotFoundException: Exception
    {
        public ObjectNotFoundException(): base() { }
    }
}
