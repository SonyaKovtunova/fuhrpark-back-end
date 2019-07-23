using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<CarSubgroupDto> CarSubgroups { get; set; }
    }
}
