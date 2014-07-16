using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.Model.Interfaces;

namespace MyChat.DataAccess.Interfaces
{
    public interface IDb : IDisposable
    {
        IClient LoadClient(Guid id);
        IClient SaveClient(IClient o);

        IMessage LoadMessage(Guid id);
        IMessage SaveMessage(IMessage o, Guid? clientId = null);

        IParticipant LoadParticipant(Guid clientId, Guid sessionId);
        IParticipant SaveParticipant(IParticipant o);

        IPractice LoadPractice(Guid id);
        IPractice SavePractice(IPractice o);

        ISession LoadSession(Guid id);
        ISession SaveSession(ISession o);

        IList<IMessage> LoadMessagesForSession(Guid sessionId);
        IList<IClient> LoadClientsForPractice(Guid practiceId);
        IList<IParticipant> LoadParticipantsForSession(Guid sessionId);
    }
}
