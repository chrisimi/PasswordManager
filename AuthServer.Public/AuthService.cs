using AuthServer.Domain.db;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace AuthServer.Public
{
    /// <summary>
    /// Represents the values which are used in the UserAuthSuccessfulEvent
    /// </summary>
    /// <param name="userId">the id of the user which logged in through the AuthServer</param>
    public delegate void UserAuthSuccessful(Guid userId);
    public class AuthService
    {
        /// <summary>
        /// The event which will be executed when a user successfully logged in through the authserver pipline
        /// </summary>
        public event UserAuthSuccessful UserAuthSuccessfulEvent;

        private Client _client;
        private string _authServerIP;
        private LoginProcess _currentLoginProcess;
        private Timer timer = new Timer();
        /// <summary>
        /// Creates a new AuthService instance 
        /// </summary>
        /// <param name="clientId">the id of the registered client in the authserver panel</param>
        /// <param name="authServerIp">the ip of the authserver used</param>
        public AuthService(Guid clientId, string authServerIp)
        {
            //the system works with a url without a ending '/'
            if (authServerIp.EndsWith("/"))
                authServerIp = authServerIp.Substring(0, authServerIp.Length - 1);

            this._client = RestHelper.GetData<Client>(authServerIp + "/api/client", new Dictionary<string, string>()
            {
                { "id", clientId.ToString() }
            }).Result;
            this._authServerIP = authServerIp;
        }

        /// <summary>
        /// request an outh by the authserver pipeline
        /// </summary>
        /// <returns>the url where the user can loggin in the authserver pipeline</returns>
        public async Task<string> RequestAuth()
        {
            _currentLoginProcess = await RestHelper.PostData<LoginProcess>(_authServerIP + "/api/createloginprocess", new Dictionary<string, string>()
            {
                { "clientId", _client.Client_Id.ToString() }
            });

            if (_currentLoginProcess == null)
                throw new Exception("error while requesting auth");
            
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            return _authServerIP + "/home/authenticatebyprocess?processId=" + _currentLoginProcess.Id.ToString();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoginProcess loginProcess = await RestHelper.GetData<LoginProcess>(_authServerIP + "/api/get/loginprocess/" + _currentLoginProcess.Id.ToString(), null);
            if(loginProcess != null)
            {
                if (loginProcess.Successful && loginProcess.UserId != Guid.Empty)
                {
                    UserAuthSuccessfulEvent?.Invoke(loginProcess.UserId);
                    timer.Stop();
                }
            }
        }
    }
}
