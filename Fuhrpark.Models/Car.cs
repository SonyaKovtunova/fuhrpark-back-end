using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public int TypId { get; set; }

        public int ManufacturerId { get; set; }

        public string Model { get; set; }

        public string Color{ get; set; }

        public string ChassisNumber { get; set; }

        public bool Decommissioned { get; set; }

        public virtual Typ Typ { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual CarSpec CarSpec { get; set; }

        public virtual CarBusiness CarBusiness { get; set; }
    }
}
