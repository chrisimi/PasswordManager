using MySql.Data.MySqlClient;
using PasswordManager.Database;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Database
{
    class Program
    {

        static void Main(string[] args)
        {
        }

        public bool Check_Connection(string conn)
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

