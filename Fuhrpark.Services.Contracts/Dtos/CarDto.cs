using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string ChassisNumber { get; set; }

        public bool Decommissioned { get; set; }

        public int TypId { get; set; }

        public TypDto Typ { get; set; }

        public int ManufacturerId { get; set; }

        public ManufacturerDto Manufacturer { get; set; }

        public CarSpecDto CarSpec { get; set; }

        public CarBusinessDto CarBusiness { get; set; }

        public class CarSpecDto
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

            public int FuelId { get; set; }

            public FuelDto Fuel { get; set; }

            public int EngineOilId { get; set; }

            public EngineOilDto EngineOil { get; set; }

            public int GearOilId { get; set; }

            public GearOilDto GearOil { get; set; }
        }

        public class CarBusinessDto
        {
            public string Location { get; set; }

            public DateTime CreateDate { get; set; }

            public DateTime? UpdateDate { get; set; }

            public int? UserId { get; set; }

            public UserDto User { get; set; }
        }
    }
}
