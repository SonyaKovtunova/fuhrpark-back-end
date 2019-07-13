using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class Fuel: CommonEntity
    {
        public virtual IEnumerable<CarSpec> Cars { get; set; }
    }
}
