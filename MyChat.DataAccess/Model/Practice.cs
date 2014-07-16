using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyChat.Model.Interfaces;

namespace MyChat.DataAccess.Model
{
    [Table("Practice")]
    public partial class Practice : IPractice
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
