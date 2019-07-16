using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos.Common
{
    public class CommonAddDto
    {
        [Required]
        public string Name { get; set; }
    }
}
