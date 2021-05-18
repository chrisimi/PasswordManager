using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DBModule.Common
{
    public class Mapping : Attribute
    {
        public string DbField { get; private set; }
        public DbType DbType { get; private set; }

        public Mapping(string dbField, DbType dbType = DbType.String)
        {
            DbField = dbField;
            DbType = dbType;
        }
    }
}
