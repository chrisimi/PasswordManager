using AuthServer.Domain.db;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain.interfaces
{
    public interface ILogic
    {
        T GetById<T>(Guid id) where T : DBObject;
        T Add<T>(T value, bool overwrite = false) where T : DBObject;

        bool Delete<T>(Guid id) where T : DBObject;
        MicrosoftInformation GetMsInfoByMicrosoftId(Guid id);
        User GetUserByMSInfoId(Guid id);
        bool CreateDatabase();
        bool IsOnline();
        List<Client> GetClientsFromUser(Guid userid);
    }
}
