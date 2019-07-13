using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class CarModel
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string ChassisNumber { get; set; }

        public bool Decommissioned { get; set; }

        public TypModel Typ { get; set; }

        public ManufacturerModel Manufacturer { get; set; }
    }
}
