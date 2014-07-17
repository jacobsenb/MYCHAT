using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface IParticipantInfo : IParticipant
    {
        string PracticeName { get; set; }
        string ClientName { get; set; }
        string ClientEmail { get; set; }
    }
}
