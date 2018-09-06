using Polyglot.DataAccess.Helpers;
using System.Collections.Generic;

namespace Polyglot.DataAccess.Entities.Chat
{
    public class ChatDialog : Entity
    {
        public long Identifier { get; set; }

        public ChatGroup DialogType { get; set; }

        public string DialogName { get; set; }

        public virtual ICollection<DialogParticipant> DialogParticipants { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }

        public ChatDialog()
        {
            Messages = new List<ChatMessage>();
            DialogParticipants = new List<DialogParticipant>();
        }
    }
}
