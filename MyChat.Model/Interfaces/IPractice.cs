using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChat.Model.Interfaces
{
    public interface IPractice
    {
        Guid PracticeId { get; set; }
        string Name { get; set; }
    }
}
