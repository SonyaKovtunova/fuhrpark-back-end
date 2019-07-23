using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarSubgroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CarInfo> Cars { get; set; }

        public class CarInfo
        {
            public int Id { get; set; }

            public string RegistrationNumber { get; set; }

            public string Model { get; set; }

            public TypDto Typ { get; set; }

            public ManufacturerDto Manufacturer { get; set; }
        }
    }   
}
