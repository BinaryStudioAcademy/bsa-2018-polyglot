namespace Polyglot.DataAccess.Entities.Chat
{
    public class DialogParticipant
    {
        public int? ChatDialogId { get; set; }
        public virtual ChatDialog ChatDialog { get; set; }

        public int? ParticipantId { get; set; }
        public virtual UserProfile Participant { get; set; }
    }
}
