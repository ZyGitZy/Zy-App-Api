﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zy.User.App.Models
{
    public class ChangePasswordDto
    {
        public string OriginPassword { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
