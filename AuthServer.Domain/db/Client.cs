using DBModule.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain.db
{
    public class Client : DBObject
    {
        [Mapping("client_id", System.Data.DbType.Guid)]
        public Guid Client_Id { get; set; }
        [Mapping("date_registered", System.Data.DbType.DateTime)]
        public DateTime DateRegistered { get; set; }
        [Mapping("name", System.Data.DbType.String)]
        public string Name { get; set; }
        [Mapping("microsoftclient_id", System.Data.DbType.Guid)]
        public Guid MicrosoftClient_Id { get; set; }
        [Mapping("tenant_id", System.Data.DbType.Guid)]
        public Guid Tenant_Id { get; set; }
        [Mapping("tenant", System.Data.DbType.String)]
        public string Tenant { get; set; }
        [Mapping("redirect_uri", System.Data.DbType.String)]
        public string RedirectUri { get; set; }
        [Mapping("owner_id", System.Data.DbType.Guid)]
        public Guid OwnerId { get; set; }

        public string GetTenant()
        {
            if (Tenant_Id != Guid.Empty)
                return Tenant_Id.ToString();

            return Tenant;
        }
    }
}
