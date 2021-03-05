using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Domain
{
    public class OverviewObject
    {
        public string Key { get; set; }
        public DateTime Changed { get; set; }

        public static implicit operator OverviewObject(Entry entry)
        {
            if (entry == null)
                return null;
            return new OverviewObject()
            {
                Key = entry.Key,
                Changed = entry.Changed
            };
        }
    }
}
