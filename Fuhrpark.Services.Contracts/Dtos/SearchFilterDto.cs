using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class SearchFilterDto
    {
        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string ChassisNumber { get; set; }

        public bool? Decommissioned { get; set; }

        public int? TypId { get; set; }

        public int? ManufacturerId { get; set; }

        public CarSpecSearchDto CarSpec { get; set; }

        public CarBusinessSearchDto CarBusiness { get; set; }

        public class CarSpecSearchDto
        {
            public int? MinPerformance { get; set; }

            public int? MaxPerformance { get; set; }

            public int? MinEngineDisplacement { get; set; }

            public int? MaxEngineDisplacement { get; set; }

            public int? MinSpeed { get; set; }

            public int? MaxSpeed { get; set; }

            public int? MinTotalWeight { get; set; }

            public int? MaxTotalWeight { get; set; }

            public string EngineCode { get; set; }

            public DateTime? ProductionStartDate { get; set; }

            public DateTime? ProductionEndDate { get; set; }

            public DateTime? RegistrationStartDate { get; set; }

            public DateTime? RegistrationEndDate { get; set; }

            public bool? Catalyst { get; set; }

            public bool? HybridDrive { get; set; }

            public int? FuelId { get; set; }

            public int? EngineOilId { get; set; }

            public int? GearOilId { get; set; }
        }

        public class CarBusinessSearchDto
        {
            public string Location { get; set; }

            public DateTime? CreateStartDate { get; set; }

            public DateTime? CreateEndDate { get; set; }

            public DateTime? UpdateStartDate { get; set; }

            public DateTime? UpdateEndDate { get; set; }

            public int? UserId { get; set; }
        }
    }
}
