using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordManager.XML
{
    public class XmlLogic : ILogic
    {
        private Dictionary<string, Entry> entries;


        private string GetFilePathByUserID(Guid id)
        {
            string filename = string.Format("{0}.xml", id);
            return Path.Combine(".", filename);
        }


        private void SaveXml(string filename)
        {
            if (entries == null)
                return;

            UserEntries data = new UserEntries();
            data.Items = entries.Values.ToList();

            using (Stream s = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserEntries));
                serializer.Serialize(s, data);
            }
        }

        private void LoadXml(string filename)
        {
            entries = new Dictionary<string, Entry>();

            if (File.Exists(filename))
            {
                UserEntries result = null;
                using (Stream s = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(UserEntries));
                    result = serializer.Deserialize(s) as UserEntries;
                }

                if (result != null && result.Items != null)
                {
                    foreach (Entry e in result.Items)
                    {
                        entries.Add(e.Key, e);
                    }
                }
            }
        }


        public void Add(Entry entry)
        {
            if (entry == null)
                return;

            LoadXml(GetFilePathByUserID(entry.UserId));
            if (!entries.ContainsKey(entry.Key))
            {
                entries.Add(entry.Key, entry);
                SaveXml(GetFilePathByUserID(entry.UserId));
            }
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            LoadXml(GetFilePathByUserID(userId));
            return entries.Values.ToList();
        }

        public void Remove(Entry entry)
        {
            if (entry == null)
                return;

            LoadXml(GetFilePathByUserID(entry.UserId));
            if (entries.ContainsKey(entry.Key))
            {
                entries.Remove(entry.Key);
                SaveXml(GetFilePathByUserID(entry.UserId));
            }
        }

        public void Update(Entry entry)
        {
            if (entry == null)
                return;

            LoadXml(GetFilePathByUserID(entry.UserId));
            if (entries.ContainsKey(entry.Key))
            {
                entries.Remove(entry.Key);
                entries.Add(entry.Key, entry);
                SaveXml(GetFilePathByUserID(entry.UserId));
            }
        }
    }
}
