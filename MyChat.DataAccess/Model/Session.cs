using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyChat.Model.Interfaces;

namespace MyChat.DataAccess.Model
{
    [Table("Session")]
    public partial class Session : ISession
    {
        public Session()
        {
            Messages = new HashSet<Message>();
            Participants = new HashSet<Participant>();
        }

        public Guid SessionId { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public string Topic { get; set; }

        public DateTime StartDateTime { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
