using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class MessageDto : IMessage
    {
        public MessageDto() { }

        public MessageDto(IMessage o)
        {
            MessageId = o.MessageId;
            MessageText = o.MessageText;
            PostDateTime = o.PostDateTime;
            SessionId = o.SessionId;
            ParticipantId = o.ParticipantId;
        }

        public Guid MessageId { get; set; }
        public string MessageText { get; set; }
        public DateTime PostDateTime { get; set; }
        public Guid SessionId { get; set; }
        public Guid ParticipantId { get; set; }
    }
}
