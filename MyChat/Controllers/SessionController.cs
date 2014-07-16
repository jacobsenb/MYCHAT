using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using MyChat.DataAccess;
using MyChat.Model;
using MyChat.Model.Interfaces;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Session")]
    public class SessionController : ApiController
    {
        [HttpGet]
        [Route("{guid}")]
        public SessionDto Get(Guid guid)
        {
            using (var db = new Db())
            {
                var o = db.LoadSession(guid);
                if (o == null) return null;
                return new SessionDto(o);
            }
        }

        [HttpPost]
        [Route("")]
        public SessionDto Post([FromBody]SessionDto value)
        {
            using (var db = new Db())
            {
                return new SessionDto(db.SaveSession(value));
            }
        }
    }
}
