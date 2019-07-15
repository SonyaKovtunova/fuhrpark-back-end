using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class RemoveCarSettingsDto
    {
        public int CarId { get; set; }

        public bool IsCheck { get; set; }

        public bool ManufacturerIsDelete { get; set; }

        public bool TypIsDelete { get; set; }

        public bool FuelIsDelete { get; set; }

        public bool EngineOilIsDelete { get; set; }

        public bool GearOilIsDelete { get; set; }

        public bool UserIsDelete { get; set; }
    }
}
