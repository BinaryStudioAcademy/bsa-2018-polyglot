using System;

namespace Polyglot.DataAccess.Entities.Chat
{
    public class Message : Entity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int FromId { get; set; }

        public string FromFullName { get; set; }

        public DateTime Date { get; set; }

        public bool IsReceived { get; set; }

        public bool IsRead { get; set; }
    }
}
