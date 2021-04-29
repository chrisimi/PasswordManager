using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PasswordManager.Domain
{
    public interface ILogic
    {
        void Add(Entry entry);
        void Remove(Entry entry);
        void Update(Entry entry);
        IList<Entry> GetFromUser(Guid userId);
        void GetFromUser(TextBox newUser);
    }
}
