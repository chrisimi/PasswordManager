using System;
using System.Data.Common;

namespace DBModule.Common
{
    public interface IDatabase
    {
        DbConnection Connection { get; }

        void Open();
        void Close();
        IDBDataProvider<T> DataSet<T>() where T : new();
    }
}
