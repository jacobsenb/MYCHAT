using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyChat.DataAccess.Model
{
    [Table("Practice")]
    public partial class Practice
    {
        public Practice()
        {
            Clients = new HashSet<Client>();
        }

        public Guid PracticeId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
