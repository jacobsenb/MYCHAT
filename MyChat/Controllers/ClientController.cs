using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyChat.Model;
using MyChat.DataAccess.Interfaces;
using MyChat.DataAccess;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Practice")]
    public class ClientController : ApiController
    {
        [HttpGet]
        [Route("{practiceId}/Clients")]
        public IEnumerable<ClientDto> GetClientsForPractice(Guid practiceId)
        {
            using (var db = (IDb)new Db())
            {
                var data = db.LoadClientsForPractice(practiceId);
                foreach (var d in data)
                    yield return new ClientDto(d);
            }
        }

        [HttpPost]
        [Route("{practiceId}/Clients")]
        public ClientDto Post(Guid practiceId, [FromBody] ClientDto value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (practiceId != value.PracticeId)
                throw new ArgumentException("PracticeId mismatch");
            using (var db = (IDb)new Db())
            {
                var o = db.SaveClient(new ClientDto(value));
                if (o == null) return null;
                return new ClientDto(o);
            }
        }
    }
}
