using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class GearOil: CommonEntity
    {
        public virtual IEnumerable<CarSpec> Cars { get; set; }
    }
}
