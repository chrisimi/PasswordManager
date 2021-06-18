using MySql.Data.MySqlClient;
using PasswordManager.Database;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManager.Domain;

namespace PasswordManager.Database
{
    class Program
    {

        static void Main(string[] args)
        {
            Guid userId = Guid.Parse("36762779-87d1-4f63-9bc4-f86bfa6cc6e7");
            DBConnection db = new DBConnection();
            //db.Add(new Domain.Entry()
            //{
            //    Key = "Amazon",
            //    UserId = userId,
            //    Password = "abc123"
            //});

            var result = db.GetFromUser(userId);
            OutputList(result);
            Entry entry = result.ElementAt(0);
            entry.Password = "test1bc";
            db.Update(entry);
            var result2 = db.GetFromUser(userId);
            OutputList(result2);


            Console.ReadLine();
        }

        private static void OutputList(IList<Entry> entries)
        {
            foreach(var entry in entries)
            {
                Console.WriteLine($"Passwort: {entry.Password}");
            }
        }

        public static bool Check_Connection(string conn)
        {
            bool result = false;
            MySqlConnection connection = new MySqlConnection(conn);

            try
            {
                Console.WriteLine("Openning Connection ");
                connection.Open();

                result = true;
               
                Console.WriteLine("Connection successful");
                Console.ReadLine();
                connection.Close();
                
            }
            catch 
            {
                result = false;
                Console.ReadLine();
            }
            
            return result;
           
            
        }
       

    }
        
}

