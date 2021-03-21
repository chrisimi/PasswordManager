using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Web
{
    public class TestLogic : ILogic
    {
        private List<Entry> _entries = new List<Entry>()
        {
            new Domain.Entry()
            {
                Key = "google.com",
                Email = "chrisi@gmail.com",
                Password = "pwd1",
                Notes = "achtung virus",
                UserId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new Domain.Entry()
            {
                Key = "willtreffen.at",
                Email ="essa@hak.at",
                Password = "burger123",
                Notes = "achtung virus",
                UserId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            }
        };

        public void Add(Entry entry)
        {
            _entries.Add(entry);
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            return _entries.Where(a => a.UserId == userId).ToList();
        }

        public void Remove(Entry entry)
        {
            foreach(var obj in _entries)
            {
                if(obj.Key == entry.Key)
                {
                    _entries.Remove(obj);
                    return;
                }
            }
        }

        public void Update(Entry entry)
        {
            foreach(var obj in _entries)
            {
                if (obj.Key == entry.Key)
                {
                    _entries.Remove(obj);
                    _entries.Add(entry);
                    return;
                }
            }
        }
    }
}