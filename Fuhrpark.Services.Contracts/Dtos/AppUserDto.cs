using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Services.Contracts.Dtos
{
    public class AppUserDto
    {
        public Int32 Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String Mobile { get; set; }

        public String Password { get; set; }

        public String RefreshToken { get; set; }

        public DateTime CreateDate { get; set; }

        public Boolean IsActive { get; set; }
    }
}
