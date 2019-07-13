using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class CarAddModel
    {
        [Required]
        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string ChassisNumber { get; set; }

        public bool Decommissioned { get; set; }

        [Required]
        public int TypId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public CarSpecModel CarSpec { get; set; }

        [Required]
        public CarBusinessModel CarBusiness { get; set; }
    }
}
