using System;

namespace Polyglot.DataAccess.Entities.Chat
{
    public class ChatMessage : Entity
    {
        public int SenderId { get; set; }

        public virtual UserProfile Sender { get; set; }

        public string Body { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsRead { get; set; }

        public virtual ChatDialog Dialog { get; set; }

        public int? DialogId { get; set; }
    }
}
