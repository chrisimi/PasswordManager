using DBModule.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AuthServer.Domain.db
{
    public class LoginProcess : DBObject
    {
        [Mapping("id", DbType.Guid)]
        public Guid Id { get; set; }

        [Mapping("user_id", DbType.Guid)]
        public Guid UserId { get; set; }

        [Mapping("client_id", DbType.Guid)]
        public Guid ClientId { get; set; }

        [Mapping("successful", DbType.Boolean)]
        public bool Successful { get; set; }

        [Mapping("provider", DbType.Int32)]
        public Provider Provider { get; set; }

        [Mapping("origin", DbType.Int32)]
        public Origin Origin { get; set; }

        [Mapping("created", DbType.DateTime)]
        public DateTime Created { get; set; }
    }
}
