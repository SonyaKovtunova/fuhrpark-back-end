using System;
using System.Collections.Generic;
using System.Text;

namespace Fuhrpark.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String Password { get; set; }

        public String Mobile { get; set; }

        public DateTime CreateDate { get; set; }

        public String RefreshToken { get; set; }

        public DateTime? RefreshTokenExpires { get; set; }

        public String ForgotPasswordCodeToken { get; set; }

        public Boolean IsActive { get; set; }
    }
}
