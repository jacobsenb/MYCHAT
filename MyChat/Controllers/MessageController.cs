using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyChat.DataAccess;
using MyChat.DataAccess.Interfaces;
using MyChat.Model;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Session")]
    public class MessageController : ApiController
    {
        [HttpGet]
        [Route("{sessionId}/Messages")]
        public IEnumerable<MessageDto> GetMessagesForSession(Guid sessionId)
        {
            using (var db = (IDb) new Db())
            {
                var data = db.LoadMessagesForSession(sessionId);
                foreach (var d in data) 
                    yield return new MessageDto(d);
            }
        }

        [HttpPost]
        [Route("{sessionId}/Messages")]
        public MessageDto CreateMessageForSession(Guid sessionId, [FromBody] MessageDto value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (sessionId != value.SessionId)
                throw new ArgumentException("sessionId mismatch");
            using (var db = (IDb)new Db())
            {
                var o = db.SaveMessage(new MessageDto(value));
                if (o == null) return null;
                return new MessageDto(o);
            }
        }
    }
}
