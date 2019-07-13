using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class CarSpec
    {
        public int Id { get; set; }

        public int FuelId { get; set; }

        public int? Performance { get; set; }

        public int? EngineDisplacement { get; set; }

        public int? MaxSpeed { get; set; }

        public int? TotalWeight { get; set; }

        public string EngineCode { get; set; }

        public int EngineOilId { get; set; }

        public int GearOilId { get; set; }

        public DateTime? ProductionDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public bool Catalyst { get; set; }

        public bool HybridDrive { get; set; }

        public virtual Car Car { get; set; }

        public virtual Fuel Fuel { get; set; }

        public virtual EngineOil EngineOil { get; set; }

        public virtual GearOil GearOil { get; set; }
    }
}
