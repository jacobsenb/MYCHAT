using System;
using System.Web.Http;
using MyChat.DataAccess;
using MyChat.DataAccess.Interfaces;
using MyChat.Model;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Practice")]
    public class PracticeController : ApiController
    {
        [HttpGet]
        [Route("{guid}")]
        public PracticeDto Get(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentNullException("guid");
            using (var db = (IDb)new Db())
            {
                var o = db.LoadPractice(guid);
                if (o == null) return null;
                return new PracticeDto(o);
            }
        }

        [HttpPost]
        [Route("")]
        public PracticeDto Post([FromBody]PracticeDto value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            using (var db = (IDb)new Db())
            {
                return new PracticeDto(db.SavePractice(value));
            }
        }
    }
}
