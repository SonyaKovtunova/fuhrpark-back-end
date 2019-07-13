using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class Typ: CommonEntity
    {
        public virtual IEnumerable<Car> Cars { get; set; }
    }
}
