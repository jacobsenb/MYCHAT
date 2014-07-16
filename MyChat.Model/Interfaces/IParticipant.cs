using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface IParticipant
    {
        Guid ParticipantId { get; set; }
        bool Accepted { get; set; }
        Guid ClientId { get; set; }
        Guid SessionId { get; set; }
    }
}
