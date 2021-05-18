using AuthServer.Domain.db;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServer.Domain.interfaces
{
    /// <summary>
    /// the main interface which is for authentification between AuthServer and the providers
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// get the token from the user
        /// </summary>
        /// <param name="user">the user from which to get the token</param>
        /// <returns>the token nur null if it wasn't successful</returns>
        string GetToken(User user);

        string GetRedirectionURL(Client client, LoginProcess loginProcess);
    }
}
