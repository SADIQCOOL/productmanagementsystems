﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace productmanagementsystems.Models
{
    public class TempDriverUser
    {
        [Required]
        [Display(Name = "Contact")]
        public string DContactNo { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}