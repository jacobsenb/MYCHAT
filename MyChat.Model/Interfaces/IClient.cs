using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface IClient
    {
        Guid ClientId { get; set; }
        string Name { get; set; }
        string Email { get; set; }        
        Guid PracticeId { get; set; }
    }
}
