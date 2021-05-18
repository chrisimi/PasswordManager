using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DBModule.Common
{
    public abstract class ADatabase : IDatabase
    {
        protected DbConnection _connection = null;

        public string ConnectionString { get; set; }
        public ADatabase() : this("") { }

        public ADatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = GetInstance(ConnectionString);
                }
                Open();
                return _connection;
            }
        }
        protected abstract DbConnection GetInstance(string connectionstring);

        public virtual void Close()
        {
            try
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            catch
            {

            }
        }

        public virtual void Open()
        {
            try
            {
                if (_connection == null)
                    _connection = GetInstance(ConnectionString);

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public abstract IDBDataProvider<T> DataSet<T>() where T : new();
    }
}
