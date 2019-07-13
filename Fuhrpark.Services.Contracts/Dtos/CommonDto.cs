using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class CommonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
