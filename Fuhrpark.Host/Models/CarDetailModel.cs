using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class CarDetailModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string ChassisNumber { get; set; }

        public bool Decommissioned { get; set; }

        public TypModel Typ { get; set; }

        public ManufacturerModel Manufacturer { get; set; }

        public CarSpecModel CarSpec { get; set; }

        public CarBusinessModel CarBusiness { get; set; }

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

            public FuelModel Fuel { get; set; }

            public EngineOilModel EngineOil { get; set; }

            public GearOilModel GearOil { get; set; }
        }

        public class CarBusinessModel
        {
            public string Location { get; set; }

            public DateTime CreateDate { get; set; }

            public DateTime? UpdateDate { get; set; }

            public UserModel User { get; set; }
        }
    }
}
