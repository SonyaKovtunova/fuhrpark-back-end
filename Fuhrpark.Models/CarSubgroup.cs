﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class CarSubgroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual List<CarInCarSubgroup> CarInCarSubgroups { get; set; }
    }
}
