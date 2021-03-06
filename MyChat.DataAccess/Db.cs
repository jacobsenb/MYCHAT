﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyChat.DataAccess.Interfaces;
using MyChat.DataAccess.Model;
using MyChat.Model;
using MyChat.Model.Interfaces;

namespace MyChat.DataAccess
{
    public class Db : IDb
    {
        private readonly MyChatContext _context = new MyChatContext();

        public IClient LoadClient(Guid id)
        {
            return _context.Clients.SingleOrDefault(o => o.ClientId == id);
        }

        public IClient SaveClient(IClient o)
        {
            var client = _context.Clients.Create();
            client.ClientId = o.ClientId;
            client.Email = o.Email;
            client.Name = o.Name;
            client.PracticeId = o.PracticeId;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return client;
        }

        public IMessage LoadMessage(Guid id)
        {
            return _context.Messages.SingleOrDefault(o => o.MessageId == id);
        }

        public IMessage SaveMessage(IMessage o, Guid? clientId = null)
        {
            if (clientId != null)
            { // user message
                var participant = _context.Participants.SingleOrDefault(c => c.SessionId == o.SessionId && c.ClientId == clientId);
                if (participant == null)
                    throw new InvalidOperationException("Couldn't get participant");
                if (o.ParticipantId == null)
                    o.ParticipantId = participant.ParticipantId;
                else if (o.ParticipantId != participant.ParticipantId)
                    throw new InvalidOperationException("Invalid participantId");    
            }
            //else
            //  system message

            var message = _context.Messages.Create();
            message.MessageId = o.MessageId;
            message.MessageText = o.MessageText;
            message.ParticipantId = o.ParticipantId;
            message.PostDateTime = o.PostDateTime;
            message.SessionId = o.SessionId;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public IParticipant LoadParticipant(Guid clientId, Guid sessionId)
        {
            return _context.Participants.SingleOrDefault(o => o.ClientId == clientId && o.SessionId == sessionId);
        }

        public IParticipant SaveParticipant(IParticipant o)
        {
            var isNew = false;
            var participant = _context.Participants.SingleOrDefault(p => p.ParticipantId == o.ParticipantId);
            if (participant == null)
            {
                participant = _context.Participants.Create();
                isNew = true;
            }                            
            participant.ParticipantId = o.ParticipantId;
            participant.ClientId = o.ClientId;
            participant.Accepted = o.Accepted;
            participant.SessionId = o.SessionId;

            if (isNew)
                _context.Participants.Add(participant);

            _context.SaveChanges();
            return participant;
        }

        public IPractice LoadPractice(Guid id)
        {
            return _context.Practices.SingleOrDefault(o => o.PracticeId == id);
        }

        public IPractice SavePractice(IPractice o)
        {
            var practice = _context.Practices.Create();
            practice.PracticeId = o.PracticeId;
            practice.Name = o.Name;
            _context.Practices.Add(practice);
            _context.SaveChanges();
            return practice;
        }

        public ISession LoadSession(Guid id)
        {
            return _context.Sessions.SingleOrDefault(o => o.SessionId == id);
        }

        public ISession SaveSession(ISession o)
        {
            var session = _context.Sessions.Create();
            session.SessionId = o.SessionId;
            session.Owner = o.Owner;
            session.Topic = o.Topic;
            session.StartDateTime = o.StartDateTime;
            _context.Sessions.Add(session);
            _context.SaveChanges();
            return session;
        }

        public IList<IMessage> LoadMessagesForSession(Guid sessionId)
        {
            return _context.Messages.Where(o => o.SessionId == sessionId).ToList().Cast<IMessage>().ToList();
        }

        public IList<IClient> LoadClientsForPractice(Guid practiceId)
        {
            return _context.Clients.Where(o => o.PracticeId == practiceId).ToList().Cast<IClient>().ToList();
        }

        public IList<IParticipant> LoadParticipantsForSession(Guid sessionId)
        {
            return _context.Participants.Where(o => o.SessionId == sessionId).ToList().Cast<IParticipant>().ToList();
        }

        public IList<IParticipantInfo> LoadParticipantsInfoForSession(Guid sessionId)
        {
            return (
                from p in _context.Participants
                join c in _context.Clients on p.ClientId equals c.ClientId
                join prac in _context.Practices on p.Client.PracticeId equals prac.PracticeId
                where p.SessionId == sessionId
                select new ParticipantInfoDto
                       {
                           ParticipantId = p.ParticipantId,
                           Accepted = p.Accepted,
                           ClientId = p.ClientId,
                           SessionId = p.SessionId,
                           PracticeName = prac.Name,
                           ClientName = c.Name,
                           ClientEmail = c.Email
                       }
                ).ToList().Cast<IParticipantInfo>().ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
