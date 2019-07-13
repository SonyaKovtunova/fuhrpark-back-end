using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class CarBusinessModel
    {
        public string Location { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public UserModel User { get; set; }
    }
}
