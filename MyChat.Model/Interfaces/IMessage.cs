using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface IMessage
    {
        Guid MessageId { get; set; }
        string MessageText { get; set; }
        DateTime PostDateTime { get; set; } 
        Guid SessionId { get; set; }
        Guid ParticipantId { get; set; }
    }
}
