
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class NotificationOption
    {
        public int? NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        public int? OptionID { get; set; }
        public virtual Option Option { get; set; }
    }
}
