using MySql.Data.MySqlClient;
using MySql.Data;
using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

namespace PasswordManager.Database
{
    public delegate void MapDatabaseValues<T>(System.Data.IDataReader reader, T item);

    public class DBConnection : ILogic
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string userId;
        private string password;
        private string connectionString;
        
        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "ADD_YOUR_SERVER_URL_OR_IP_ADDRESS_HERE";
            database = "ADD_YOUR_DATABASE_NAME_HERE";
            userId = "ADD_YOUR_USERNAME_HERE";
            password = "ADD_YOUR_PASSWORD_HERE";
            
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + userId + ";" + "PASSWORD=" + password + ";";

            connectionString = String.Format("server={0}; userId={1}; password={2}; database={3}", server, userId, password, database);

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Kann sicht nicht mit dem Server verbinden");
                        break;

                    case 1045:
                        MessageBox.Show("Ungültiger Benutzername/Passwort, bitte versuchen Sie es nocheinmal");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

     
        public void Update(Entry entry)
        {
            string query = @"Update Tablename(UserId, Key, URL, Email, Password, Notes, Changed) Values(@UserId, @Key, @URL, @Email, @Password, @Notes, @Changed);";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("UserId", entry.UserId);
                cmd.Parameters.AddWithValue("Key", entry.Key);
                cmd.Parameters.AddWithValue("URL", entry.URL);
                cmd.Parameters.AddWithValue("Email", entry.Email);
                cmd.Parameters.AddWithValue("Password", entry.Password);
                cmd.Parameters.AddWithValue("Notes", entry.Notes);
                cmd.Parameters.AddWithValue("Changed", DateTime.Now);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void Remove(Entry entry)
        {
            string query = @"Delete Tablename.entry Where UserId = @UserId";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("UserId", entry.UserId);
                
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void Add(Entry entry)
        {
            string query = @"Insert Into Tablename(UserId, Key, URL, Email, Password, Notes, Changed) Values(@UserId, @Key, @URL, @Email, @Password, @Notes, @Changed);";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("UserId", entry.UserId);
                cmd.Parameters.AddWithValue("Key", entry.Key);
                cmd.Parameters.AddWithValue("URL", entry.URL);
                cmd.Parameters.AddWithValue("Email", entry.Email);
                cmd.Parameters.AddWithValue("Password", entry.Password);
                cmd.Parameters.AddWithValue("Notes", entry.Notes);
                cmd.Parameters.AddWithValue("Changed", DateTime.Now);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        
        public List<T> Select<T>(string query, MapDatabaseValues<T> mapping) where T : new()
        {
            List<T> result = new List<T>();

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        T item = new T();
                        mapping(dataReader, item);
                        result.Add(item);
                    }
                }                
                this.CloseConnection();
            }
            return result;
        }

        //public List<string>[] Select(string query)
        //{
        //    List<string>[] list = new List<string>[3];
        //    list[0] = new List<string>();
        //    list[1] = new List<string>();
        //    list[2] = new List<string>();

        //    if (OpenConnection() == true)
        //    {
        //        MySqlCommand cmd = new MySqlCommand(query, connection);
        //        MySqlDataReader dataReader = cmd.ExecuteReader();

        //        while (dataReader.Read())
        //        {
        //            list[0].Add(dataReader["id"] + "");
        //            list[0].Add(dataReader["name"] + "");
        //            list[0].Add(dataReader["age"] + "");
        //        }
        //        dataReader.Close();
        //        this.CloseConnection();
        //        return list;
        //    }
        //    else
        //    {
        //        return list;
        //    }

        //}

        private void MapEntry(IDataReader reader, Entry item)
        {
            item.Email = reader["Email"] as string;
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            IList<Entry> result = Select<Entry>("select....", MapEntry);
            return result;
        }
    }
 }