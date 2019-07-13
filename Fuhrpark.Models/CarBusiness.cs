using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class CarBusiness
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int? UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual Car Car { get; set; }

        public virtual User User { get; set; }
    }
}
