using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Routing;
using MyChat.DataAccess;
using MyChat.DataAccess.Interfaces;
using MyChat.Model;
using MyChat.Model.Interfaces;
using System.Net.Mail;

namespace MyChat.Controllers
{
    [RoutePrefix("api/Session")]
    public class SessionController : ApiController
    {
        [HttpGet]
        [Route("{guid}")]
        public SessionDto Get(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentNullException("guid");
            using (var db = (IDb)new Db())
            {
                var o = db.LoadSession(guid);
                if (o == null) return null;
                return new SessionDto(o);
            }
        }

        [HttpPost]
        [Route("")]
        public SessionDto Post([FromBody]SessionDto value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            using (var db = (IDb)new Db())
            {
                return new SessionDto(db.SaveSession(value));
            }
        }        

        [HttpPost]
        [Route("{guid}/InviteParticipants")]
        public void InviteParticipants(Guid guid)
        {
            if (guid == Guid.Empty)
                throw new ArgumentNullException("guid");

            SessionDto session = null;
            var participants = new List<ParticipantInfoDto>();
            using (var db = (IDb)new Db())
            {
                var o = db.LoadSession(guid);
                if (o == null) return;
                session = new SessionDto(o);

                var data = db.LoadParticipantsInfoForSession(guid);
                foreach (var d in data)
                    participants.Add(new ParticipantInfoDto(d));
            }
                                     
            var subjectPrefix = System.Configuration.ConfigurationManager.AppSettings["emailSubjectPrefix"];
            var landingHostPath = System.Configuration.ConfigurationManager.AppSettings["landingHostPath"];
            var landingPage = System.Configuration.ConfigurationManager.AppSettings["landingPage"];

            foreach (var p in participants)
            {
                var sessionId = p.SessionId.ToString("N");
                var clientId = p.ClientId.ToString("N");

                var subject = subjectPrefix + session.Topic;                                

                var emailBody = new StringBuilder();
                emailBody.Append("Hello ");
                emailBody.Append(p.ClientName);
                emailBody.Append(",");
                emailBody.Append("<br /><br />");
                emailBody.Append("A chat session about ");
                emailBody.Append(session.Topic);
                emailBody.Append(" has been scheduled for ");
                emailBody.Append(session.StartDateTime.ToShortDateString());
                emailBody.Append(" ");
                emailBody.Append(session.StartDateTime.ToShortTimeString());                
                emailBody.Append("<br /><br />");
                emailBody.Append("<a href='");
                emailBody.Append(landingHostPath);
                emailBody.Append(landingPage);
                emailBody.Append("?sessionId=");
                emailBody.Append(sessionId);
                emailBody.Append("&clientId=");
                emailBody.Append(clientId);
                emailBody.Append("'>");                
                emailBody.Append("Click here to chat at the appropriate time");
                emailBody.Append("</a>");
                emailBody.Append("<br />");
                emailBody.Append("<br />");

                SendHtmlEmail(p.ClientEmail, p.ClientName, subject, emailBody.ToString());
            }
        }

        private static void SendHtmlEmail(string toAddress, string toName, string subject, string body)
        {
            var smtpServer = ConfigurationManager.AppSettings["smtpServer"];
            var smtpFrom = ConfigurationManager.AppSettings["smtpFrom"];
            var smtpFromName = ConfigurationManager.AppSettings["smtpFromName"];
            
            var client = new SmtpClient(smtpServer);
            var from = new MailAddress(smtpFrom, smtpFromName, Encoding.UTF8);
            var to = new MailAddress(toAddress, toName, Encoding.UTF8);            

            var message = new MailMessage(from, to);
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;            
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;

            client.Send(message);
        }
    }
}
