using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyChat.DataAccess;
using MyChat.Model;
using MyChat.DataAccess.Interfaces;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Session")]
    public class ParticipantController : ApiController
    {
        [Route("{sessionId}/Participants")]
        public IEnumerable<ParticipantDto> GetParticipantsForSession(Guid sessionId)
        {
            using (var db = (IDb)new Db())
            {
                var data = db.LoadParticipantsForSession(sessionId);
                foreach (var d in data)
                    yield return new ParticipantDto(d);
            }
        }

        [Route("{sessionId}/Participants")]
        public ParticipantDto Post(Guid sessionId, [FromBody]ParticipantDto value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (sessionId != value.SessionId)
                throw new ArgumentException("PracticeId mismatch");
            using (var db = (IDb)new Db())
            {
                var o = db.SaveParticipant(new ParticipantDto(value));
                if (o == null) return null;
                return new ParticipantDto(o);
            }
        }
    }
}
