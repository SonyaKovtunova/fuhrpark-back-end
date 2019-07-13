using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarRemoveInfoDto
    {
        public bool WithSameManufacturer { get; set; }

        public bool WithSameTyp { get; set; }

        public bool WithSameFuel { get; set; }

        public bool WithSameEngineOil { get; set; }

        public bool WithSameGearOil { get; set; }

        public bool WithSameUser { get; set; }
    }
}
