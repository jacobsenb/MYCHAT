using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using MyChat.DataAccess;
using MyChat.DataAccess.Interfaces;
using MyChat.DataAccess.Model;
using MyChat.Model;

namespace MyChat.Hubs
{
    public class ChatHub : Hub
    {
        public class ChatUser
        {            
            public Guid SessionId { get; set; }
            public Guid ClientId { get; set; }
            public string ConnectionId { get; set; }
            public string Name { get; set; }
        }

        private static readonly object UserCacheLock = new object(); 
        private static readonly List<ChatUser> UserCache = new List<ChatUser>();

        private static void AddOrUpdateUser(ChatUser user)
        {
            lock (UserCacheLock)
            {
                var u = UserCache.SingleOrDefault(o => o.ConnectionId == user.ConnectionId);
                if (u != null)
                    UserCache.Remove(u);
                UserCache.Add(user);
            }
        }

        private static void RemoveUser(string connectionId)
        {
            lock (UserCacheLock)
            {
                var u = UserCache.SingleOrDefault(o => o.ConnectionId == connectionId);
                if (u != null)
                    UserCache.Remove(u);
            }
        }

        private static ChatUser GetUser(string connectionId)
        {
            lock (UserCacheLock)
            {
                return UserCache.SingleOrDefault(o => o.ConnectionId == connectionId);
            }
        }


        public void Echo(string message)
        {
            Clients.All.echo(message);
        }

        public void JoinSession(Guid sessionId, Guid clientId)
        {
            string clientName;
            using (var db = (IDb) new Db())
            {
                // participant has accepted
                var p = db.LoadParticipant(clientId, sessionId);
                if (p == null) return;
                p.Accepted = true;
                db.SaveParticipant(p);

                // get client name
                var client = db.LoadClient(clientId);
                if (client == null) return;
                clientName = client.Name;
            }

            AddOrUpdateUser(new ChatUser { SessionId = sessionId, ClientId = clientId, ConnectionId = Context.ConnectionId, Name = clientName });

            Groups.Add(Context.ConnectionId, sessionId.ToString("N"));

            Clients.Caller.verifyName(clientName);

            SendSystemMessage(sessionId, string.Format("{0} has joined the chat", clientName));
        }

        public override Task OnDisconnected()
        {
            var user = GetUser(Context.ConnectionId);            
            if (user != null)
            {
                var connectionId = user.ConnectionId;
                var sessionId = user.SessionId;
                var userName = user.Name;

                RemoveUser(connectionId);

                Groups.Remove(connectionId, sessionId.ToString("N"));

                SendSystemMessage(sessionId, string.Format("{0} has left the chat", userName));
            }

            return base.OnDisconnected();
        }

        public void SendMessage(Guid sessionId, Guid clientId, string message)
        {
            if (sessionId == Guid.Empty)
                throw new ArgumentNullException("sessionId");
            if (clientId == Guid.Empty)
                throw new ArgumentNullException("clientId");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            var user = GetUser(Context.ConnectionId);
            if (user == null) return;

            var m = new MessageDto
            {
                MessageId = Guid.NewGuid(),                 
                PostDateTime = DateTime.Now, 
                MessageText = message,
                ParticipantId = null, // DAL will handle this
                SessionId = sessionId, 
            };
            SaveMessageAsync(m, clientId); // user message

            Clients.Group(sessionId.ToString("N")).broadcastMessage(user.Name, message);
        }

        private void SendSystemMessage(Guid sessionId, string message)
        {
            if (sessionId == Guid.Empty)
                throw new ArgumentNullException("sessionId");
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            var m = new MessageDto
            {
                MessageId = Guid.NewGuid(),
                PostDateTime = DateTime.Now,
                MessageText = message,
                ParticipantId = null, // DAL will handle this
                SessionId = sessionId,
            };
            SaveMessageAsync(m, null); // sys message

            Clients.Group(sessionId.ToString("N")).broadcastMessage(null, message);
        }

        private static void SaveMessageAsync(MessageDto m, Guid? clientId)
        {
            Task.Run(() =>
            {
                using (var db = new Db())
                {
                    db.SaveMessage(m, clientId);
                }
            }); 
        }
    }
}