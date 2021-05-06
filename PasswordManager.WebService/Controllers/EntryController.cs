using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PasswordManager.Domain;
using PasswordManager.Web;

namespace PasswordManager.WebService.Controllers
{
    
    public class EntryController : ApiController
    {

        private static ILogic _logic = new TestLogic();

        [Route("entry/add")]
        public void PostEntry([FromBody] Entry entry) => _logic.Add(entry);

        [Route("entry/remove")]
        public void DeleteEntry([FromBody] Entry entry) => _logic.Remove(entry);

        [Route("entry/update")]
        public void PutEntry([FromBody] Entry entry) => _logic.Update(entry);

        [Route("entry/get")]
        public IList<Entry> Get(Guid id) => _logic.GetFromUser(id);



    }
}
