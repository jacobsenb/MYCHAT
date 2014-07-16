using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyChat.Model.Interfaces;

namespace MyChat.DataAccess.Model
{
    [Table("Message")]
    public partial class Message : IMessage
    {
        public Guid MessageId { get; set; }

        [Column("Message")]
        [Required]
        public string MessageText { get; set; }

        public DateTime PostDateTime { get; set; }

        public Guid SessionId { get; set; }

        public Guid? ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }

        public virtual Session Session { get; set; }
    }
}
