﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPED.Application.DTOs.Identity
{
    public class TokenRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
