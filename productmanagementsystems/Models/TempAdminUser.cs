using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace productmanagementsystems.Models
{
    public class TempAdminUser
    {
        [Required]
        [DataType(DataType.Text)]

        public string Admin_name { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Admin_password { get; set; }

    }
}