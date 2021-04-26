using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PasswordManager.Web.Models
{
    public class AddModel
    {
        [Required]
        public string Key { get; set; }
        [Required]
        [RegularExpression("[^@ \t\r\n]+@[^@ \t\r\n]+\\.[^@ \t\r\n]+", ErrorMessage = "This is not a valid email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Notes { get; set; }
    }
}