using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class CarSubgroupInCarGroup
    {
        public int CarSubgroupId { get; set; }

        public int CarGroupId { get; set; }

        public virtual CarSubgroup CarSubgroup { get; set; }

        public virtual CarGroup CarGroup { get; set; }
    }
}
