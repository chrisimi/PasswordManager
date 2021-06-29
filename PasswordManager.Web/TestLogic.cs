using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Web
{
    public class TestLogic : ILogic
    {
        private List<Entry> _entries = new List<Entry>();

        public void Add(Entry entry)
        {
            entry.Changed = DateTime.Now;
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
                    entry.Changed = DateTime.Now;
                    return;
                }
            }
        }
    }
}