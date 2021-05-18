using DBModule.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain.db
{
    public class User : DBObject
    {
        [Mapping("user_id", System.Data.DbType.Guid)]
        public Guid User_Id { get; set; }
        [Mapping("microsoft_information_id", System.Data.DbType.Guid)]
        public Guid? MicrosoftInformationID { get; set; }
    }
}
