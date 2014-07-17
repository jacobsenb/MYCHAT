using MyChat.Model.Interfaces;

namespace MyChat.Model
{
    public class ParticipantInfoDto : ParticipantDto, IParticipantInfo
    {
        public ParticipantInfoDto() { }

        public ParticipantInfoDto(IParticipantInfo o)            
        {
            // base
            ParticipantId = o.ParticipantId;
            Accepted = o.Accepted;
            ClientId = o.ClientId;
            SessionId = o.SessionId;

            PracticeName = o.PracticeName;
            ClientName = o.ClientName;
            ClientEmail = o.ClientEmail;
        }

        public string PracticeName { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
    }
}
