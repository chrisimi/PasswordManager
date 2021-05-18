using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace DBModule.Common
{
    public abstract class ADBDataProvider<T> : IDBDataProvider<T> where T : new()
    {
        protected IDatabase db;
        protected IDictionary<string, object> dbParam = new Dictionary<string, object>();
        public ADBDataProvider(IDatabase database)
        {
            db = database;
        }

        public void ClearParameter()
        {
            dbParam.Clear();
        }
        public void SetParameter(string key, object value)
        {
            dbParam[key] = value;
        }

        protected List<PropertyInfo> GetPropertiesWithMapping()
        {
            List<PropertyInfo> mapping = new List<PropertyInfo>();
            PropertyInfo[] prop = typeof(T).GetProperties();
            for (int i = 0; i < prop.Length; i++)
            {
                Mapping a = prop[i].GetCustomAttribute(typeof(Mapping), true) as Mapping;
                if (a != null)
                    mapping.Add(prop[i]);
            }
            return mapping;
        }
        protected bool MapFields(List<PropertyInfo> mapping, DbDataReader reader, T values)
        {
            foreach (PropertyInfo info in mapping)
            {
                Mapping a = info.GetCustomAttribute(typeof(Mapping), true) as Mapping;
                switch (a.DbType)
                {
                    case DbType.String:
                        info.SetValue(values, reader[a.DbField] as string);
                        break;
                    case DbType.Int32:
                        info.SetValue(values, Convert.ToInt32(reader[a.DbField]));
                        break;
                    case DbType.Boolean:
                        info.SetValue(values, ((reader[a.DbField] as string).Equals("1")) ? true : false);
                        break;
                    case DbType.Decimal:
                        info.SetValue(values, Convert.ToDecimal(reader[a.DbField]));
                        break;
                    case DbType.Guid:
                        info.SetValue(values, Guid.Parse(reader[a.DbField].ToString()));
                        break;
                    case DbType.DateTime:
                        info.SetValue(values, DateTime.Parse(reader[a.DbField].ToString()));
                        break;
                }
            }
            return MapFields(reader, values);
        }

        protected virtual bool MapFields(DbDataReader reader, T values)
        {
            return true;
        }
        protected abstract void MapParameter(DbCommand command);
        protected abstract DbCommand CreateCommand(string sql);
        protected virtual DbCommand CreateTestCommand(string sql)
        {
            DbCommand cmd = CreateCommand(sql);
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public virtual int ExecuteNonQuery(string sql)
        {
            using (DbCommand cmd = CreateTestCommand(sql))
            {
                MapParameter(cmd);
                return cmd.ExecuteNonQuery();
            }
        }


        public virtual IList<T> SqlQuery(string sql)
        {
            List<T> result = new List<T>();
            List<PropertyInfo> mapping = GetPropertiesWithMapping();

            using (DbCommand cmd = CreateCommand(sql))
            {
                MapParameter(cmd);
                //-- fetch data rows
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    Debug.WriteLine("open reader for " + sql);
                    while (reader.Read())
                    {
                        T values = new T();
                        if (MapFields(mapping, reader, values))
                        {
                            result.Add(values);
                        }
                    }
                    Debug.WriteLine("close reader for " + sql);
                    reader.Close();
                }

            }
            return result;
        }
    }
}
