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
        private string table;
        
        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "pmdb";
            userId = "root";
            password = "";
            
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + userId + ";" + "PASSWORD=" + password + ";";
            table = "Entries";
            //connectionString = String.Format("server={0}; userId={1}; password={2}; database={3}", server, userId, password, database);

            connection = new MySqlConnection(connectionString);
            OpenConnection();
            CreateTable();
        }

        private void CreateTable()
        {
            string query = $"CREATE  TABLE IF NOT EXISTS {table} (" +
                "UserId char(36), " +
                "UserKey varchar(255), " +
                "URL varchar(255), " +
                "Email varchar(255), " +
                "Password varchar(255), " +
                "Notes varchar(255), " +
                "Changed datetime " +
                ");";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        private bool OpenConnection()
        {
            if (connection.State == ConnectionState.Open)
                return true;

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
            string query = $"UPDATE {table} " +
                $"SET URL = @URL, Email = @Email, Password = @Password, Notes = @Notes, Changed = @Changed " +
                $"WHERE UserKey = @Key";

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
            string query = $"Delete {table} Where UserKey = @Key";

            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("UserKey", entry.Key);
                
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public void Add(Entry entry)
        {
            string query = $"Insert Into {table}(UserId, UserKey, URL, Email, Password, Notes, Changed) Values(@UserId, @Key, @URL, @Email, @Password, @Notes, @Changed);";

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
            item.UserId = Guid.Parse(reader["UserId"].ToString());
            string a = reader["UserId"].ToString();
            string b = reader["Changed"].ToString();

            item.Key = reader["UserKey"] as string;
            item.URL = reader["URL"] as string;
            item.Email = reader["Email"] as string;
            item.Password = reader["Password"] as string;
            item.Notes = reader["Notes"] as string;
            item.Changed = DateTime.Parse(reader["Changed"].ToString());
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            IList<Entry> result = Select<Entry>($"SELECT * FROM {table} WHERE UserId='{userId.ToString()}'", MapEntry);
            return result;
        }
    }
 }