using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class EngineOil: CommonEntity
    {
        public virtual IEnumerable<CarSpec> Cars { get; set; }
    }
}
