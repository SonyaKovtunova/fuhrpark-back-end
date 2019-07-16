using Fuhrpark.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class SearchAttributeDto
    {
        public CarField CarField { get; set; }

        public ComparingType ComparingType { get; set; }

        public object Data { get; set; }
    }
}
