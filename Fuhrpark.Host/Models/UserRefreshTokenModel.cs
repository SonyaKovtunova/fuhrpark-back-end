﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuhrpark.Host.Models
{
    public class UserRefreshTokenModel
    {
        public String Token { get; set; }
        public String RefreshToken { get; set; }
    }
}
