using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Notification : Entity
    {
        public virtual UserProfile SendFrom { get; set; }
        public int SendFromId { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public int UserProfileId { get; set; }
        public string Message { get; set; }

        public virtual ICollection<NotificationOption> NotificationOptions { get; set; }

        public Notification()
        {
            NotificationOptions = new List<NotificationOption>();
        }
    }
}
