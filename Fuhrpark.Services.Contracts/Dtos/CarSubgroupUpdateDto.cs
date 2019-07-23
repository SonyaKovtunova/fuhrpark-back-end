using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CarSubgroupUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<int> CarIds { get; set; }
    }
}
