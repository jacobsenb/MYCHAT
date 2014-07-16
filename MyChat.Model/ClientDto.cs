using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class ClientDto : IClient
    {
        public ClientDto() { }

        public ClientDto(IClient o)
        {
            ClientId = o.ClientId;
            Name = o.Name;
            Email = o.Email;
            PracticeId = o.PracticeId;
        }

        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid PracticeId { get; set; }
    }
}
