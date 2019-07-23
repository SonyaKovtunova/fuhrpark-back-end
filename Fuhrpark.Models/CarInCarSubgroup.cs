using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class CarInCarSubgroup
    {
        public int CarId { get; set; }

        public int CarSubgroupId { get; set; }

        public virtual Car Car { get; set; }

        public virtual CarSubgroup CarSubgroup { get; set; }
    }
}
