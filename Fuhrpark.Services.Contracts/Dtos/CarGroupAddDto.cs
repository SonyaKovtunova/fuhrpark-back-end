using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarGroupAddDto
    {
        public string Name { get; set; }

        public IEnumerable<int> CarSubgroupIds { get; set; }
    }
}
