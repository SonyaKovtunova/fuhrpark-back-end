using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class RemoveCarRequestModel
    {
        [Required]
        public int CarId { get; set; }

        [Required]
        public bool IsCheck { get; set; }

        public bool ManufacturerIsDelete { get; set; }

        public bool TypIsDelete { get; set; }

        public bool FuelIsDelete { get; set; }

        public bool EngineOilIsDelete { get; set; }

        public bool GearOilIsDelete { get; set; }

        public bool UserIsDelete { get; set; }
    }
}
