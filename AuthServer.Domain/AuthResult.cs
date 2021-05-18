using AuthServer.Domain.db;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain
{
    public class AuthResult
    {
        public User LoggedInUser { get; set; }
        public Provider Provider { get; set; }
        public bool Success { get; set; }
    }
}
