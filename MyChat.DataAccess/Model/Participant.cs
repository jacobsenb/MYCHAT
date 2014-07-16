using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChat.DataAccess.Model
{
    [Table("Participant")]
    public partial class Participant
    {
        public Participant()
        {
            Messages = new HashSet<Message>();
        }

        public Guid ParticipantId { get; set; }

        public Guid ClientId { get; set; }

        public bool Accepted { get; set; }

        public Guid SessionId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual Session Session { get; set; }
    }
}
