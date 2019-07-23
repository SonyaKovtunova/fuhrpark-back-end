using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarAddDto
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

        public CarSpecAddDto CarSpec { get; set; }

        public CarBusinessAddDto CarBusiness { get; set; }

        public class CarSpecAddDto
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

        public class CarBusinessAddDto
        {
            public string Location { get; set; }

            public int? UserId { get; set; }
        }
    }
}
