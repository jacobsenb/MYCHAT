using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChat.DataAccess.Model
{
    [Table("Message")]
    public partial class Message
    {
        public Guid MessageId { get; set; }

        [Column("Message")]
        [Required]
        public string Message1 { get; set; }

        public DateTime PostDateTime { get; set; }

        public Guid SessionId { get; set; }

        public Guid ParticipantId { get; set; }

        public virtual Participant Participant { get; set; }

        public virtual Session Session { get; set; }
    }
}
