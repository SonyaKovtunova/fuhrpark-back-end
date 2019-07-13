using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class Manufacturer: CommonEntity
    {
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
