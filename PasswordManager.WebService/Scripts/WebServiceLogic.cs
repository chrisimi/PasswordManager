using PasswordManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.WebService.Scripts
{

    public class WebServiceLogic : ILogic
    {
        private string basicUrl;
        public WebServiceLogic(string basicUrl)
        {
            this.basicUrl = basicUrl;
        }

        public void Add(Entry entry)
        {
            _ = RestUtils.RestHelper.PostData<Entry, Entry>(basicUrl + "entry/add", null, entry).Result;
        }

        public IList<Entry> GetFromUser(Guid userId)
        {
            return RestUtils.RestHelper.GetData<IList<Entry>>(basicUrl + "entry/get", new Dictionary<string, string>()
            {
                { "id", userId.ToString() }
            }).Result;
        }

        public void Remove(Entry entry)
        {
            _ = RestUtils.RestHelper.PostData<Entry, Entry>(basicUrl + "entry/remove", null, entry);
        }

        public void Update(Entry entry)
        {
            _ = RestUtils.RestHelper.PostData<Entry, Entry>(basicUrl + "entry/update", null, entry);
        }
    }
}