using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class User: CommonEntity
    {
        public virtual IEnumerable<CarBusiness> Cars { get; set; }
    }
}
