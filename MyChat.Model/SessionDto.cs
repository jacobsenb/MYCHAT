using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class SessionDto : ISession
    {
        public SessionDto() { }

        public SessionDto(ISession o)
        {
            SessionId = o.SessionId;
            Owner = o.Owner;
            Topic = o.Topic;
            StartDateTime = o.StartDateTime;
        }

        public Guid SessionId { get; set; }
        public string Owner { get; set; }
        public string Topic { get; set; }
        public DateTime StartDateTime { get; set; }
    }
}
