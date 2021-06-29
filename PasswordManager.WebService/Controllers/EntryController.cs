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

        [Route("add")]
        public void PostEntry([FromBody] Entry entry) => _logic.Add(entry);

        [Route("remove")]
        public void DeleteEntry([FromBody] Entry entry) => _logic.Remove(entry);

        [Route("update")]
        public void PutEntry([FromBody] Entry entry) => _logic.Update(entry);

        public IList<Entry> Get(Guid id) => _logic.GetFromUser(id);
    }
}
