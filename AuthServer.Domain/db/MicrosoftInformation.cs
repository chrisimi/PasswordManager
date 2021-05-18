using DBModule.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain.db
{
    public class MicrosoftInformation : DBObject
    {
        [Mapping("msinfo_id", System.Data.DbType.Guid)]
        public Guid MsInfo_Id { get; set; }
        /// <summary>
        /// the id of the microsoft account
        /// </summary>
        [Mapping("microsoft_id", System.Data.DbType.Guid)]
        public Guid Microsoft_Id { get; set; }
        /// <summary>
        /// the primary tennant id of the user
        /// </summary>
        [Mapping("primary_tenant_id", System.Data.DbType.String)]
        public string PrimaryTenant_Id { get; set; }
        /// <summary>
        /// the token to get access to graph
        /// </summary>
        [Mapping("token", System.Data.DbType.String)]
        public string Token { get; set; }
        /// <summary>
        /// when the current token expires
        /// </summary>
        [Mapping("token_expire_date", System.Data.DbType.DateTime)]
        public DateTime TokenExpireDate { get; set; }
        /// <summary>
        /// OAuth 2.0 refresh token 
        /// </summary>
        [Mapping("refresh_token", System.Data.DbType.String)]
        public string RefreshToken { get; set; }
    }
}
