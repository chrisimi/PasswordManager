using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordManager.XML
{
    [XmlRoot("UserEntries")]
    public class UserEntries
    {
        [XmlArrayItem("Entry")]
        public List<Entry> Items { get; set; }
    }
}
