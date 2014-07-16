using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface ISession
    {
        Guid SessionId { get; set; }
        string Owner { get; set; }
        string Topic { get; set; }
        DateTime StartDateTime { get; set; }
    }
}
