using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class PracticeDto : IPractice
    {
        public PracticeDto() { }

        public PracticeDto(IPractice o)
        {
            PracticeId = o.PracticeId;
            Name = o.Name;
        }

        public Guid PracticeId { get; set; }
        public string Name { get; set; }
    }
}
