﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheDream.API.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}