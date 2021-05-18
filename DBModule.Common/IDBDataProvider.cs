using System;
using System.Collections.Generic;
using System.Text;

namespace DBModule.Common
{
    public interface IDBDataProvider<T>
    {
        void SetParameter(string key, object value);
        void ClearParameter();
        IList<T> SqlQuery(string sql);
        int ExecuteNonQuery(string sql);
    }
}
