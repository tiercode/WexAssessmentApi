﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TestUsers
{
    public class TestUser
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }    
    }
}
