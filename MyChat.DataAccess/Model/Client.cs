using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChat.DataAccess.Model
{
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            Participants = new HashSet<Participant>();
        }

        public Guid ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public Guid PracticeId { get; set; }

        public virtual Practice Practice { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }
    }
}
