using Newtonsoft.Json;
using Polyglot.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Polyglot.DataAccess.Entities
{
    public class Option : Entity
    {
        public OptionDefinition OptionDefinition { get; set; }

        public int? NotificationId { get; set; }
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }

        public Option()
        {
        }
    }
}
