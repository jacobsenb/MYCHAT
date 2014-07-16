using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class ParticipantDto : IParticipant
    {
        public ParticipantDto() { }

        public ParticipantDto(IParticipant o)
        {
            ParticipantId = o.ParticipantId;
            Accepted = o.Accepted;
            ClientId = o.ClientId;
            SessionId = o.SessionId;
        }

        public Guid ParticipantId { get; set; }
        public bool Accepted { get; set; }
        public Guid ClientId { get; set; }
        public Guid SessionId { get; set; }
    }
}
