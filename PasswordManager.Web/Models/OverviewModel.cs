using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Web.Models
{
    public class OverviewModel
    {
        public List<OverviewObject> Entries { get; set; }
    }
}