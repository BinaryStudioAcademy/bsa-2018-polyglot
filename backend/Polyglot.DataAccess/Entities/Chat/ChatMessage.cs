using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities.Chat
{
    public class ChatMessage : Entity
    {
        public int SenderId { get; set; }

        public virtual UserProfile Sender { get; set; }

        public int RecipientId { get; set; }

        public virtual UserProfile Recipient { get; set; }

        public string Body { get; set; }

        public DateTime ReceivedDate { get; set; }

        public bool IsRead { get; set; }
    }
}
