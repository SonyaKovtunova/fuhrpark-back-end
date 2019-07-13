using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class CarSpecModel
    {
        public int? Performance { get; set; }

        public int? EngineDisplacement { get; set; }

        public int? MaxSpeed { get; set; }

        public int? TotalWeight { get; set; }

        public string EngineCode { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public bool Catalyst { get; set; }

        public bool HybridDrive { get; set; }

        [Required]
        public int FuelId { get; set; }

        [Required]
        public int EngineOilId { get; set; }

        [Required]
        public int GearOilId { get; set; }
    }
}
