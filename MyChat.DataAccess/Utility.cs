//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MyChat.DataAccess.Model;
//using MyChat.Model.Interfaces;

//namespace MyChat.DataAccess
//{
//    public static class Utility
//    {
//        public static Session CreateSession(ISession session)
//        {
//            using (var db = new MyChatContext())
//            {
//                var o = db.Sessions.Create();
//                o.SessionId = session.SessionId;
//                o.Owner = session.Owner;
//                o.Topic = session.Topic;
//                o.StartDateTime = session.StartDateTime;
//                db.Sessions.Add(o);
//                db.SaveChanges();
//            }
//        }
//    }
//}
