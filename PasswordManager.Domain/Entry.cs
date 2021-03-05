using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain
{
    public class Entry
    {
        //public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Key { get; set; }
        public string URL { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Notes { get; set; }
        public DateTime Changed { get; set; }
    }
}
