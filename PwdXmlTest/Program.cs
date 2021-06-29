using PasswordManager.Domain;
using PasswordManager.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PwdXmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogic logic = new XmlLogic();

            logic.Add(new Entry() { UserId = Guid.NewGuid(), Key = "abc", Password = "1234" });


            Console.ReadLine();
        }
    }
}
