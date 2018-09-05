using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Option : Entity
    {
        public OptionDefinition OptionDefinition { get; set; }

        public virtual ICollection<NotificationOption> NotificationOptions { get; set; }

        public Option()
        {
            NotificationOptions = new List<NotificationOption>();
        }
    }
}
