using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Notification : Entity
    {
        [ForeignKey("SenderId")]
        public virtual UserProfile Sender { get; set; }
        public int? SenderId { get; set; }
        [ForeignKey("ReceiverId")]
        public virtual UserProfile Receiver { get; set; }
        public int? ReceiverId { get; set; }
        public string Message { get; set; }
        public NotificationAction NotificationAction { get; set; }
        public int Payload { get; set; }

        public virtual ICollection<Option> Options { get; set; }

        public Notification()
        {
            Options = new List<Option>();
        }
    }
}
